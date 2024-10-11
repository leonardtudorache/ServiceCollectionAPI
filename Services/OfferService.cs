using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.Offer;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces;

namespace ServiceCollectionAPI.Services
{
    public class OfferService : IOfferService
    {
        private readonly IMongoRepository<Offer> _offerRepository;
        private readonly IMongoRepository<Package> _packageRepository;
        private readonly IMapper _mapper;

        public OfferService(IMongoRepository<Offer> offerRepository, IMongoRepository<Package> packageRepository,IMapper mapper)
        {
            _offerRepository = offerRepository;
            _packageRepository = packageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OfferResponse>> GetAllOffersAsync()
        {
            var offers = await _offerRepository.FilterByAsync(o => true);
            return _mapper.Map<IEnumerable<OfferResponse>>(offers);
        }

        public async Task<OfferResponse> GetOfferByIdAsync(string id)
        {
            var offer = await _offerRepository.FindByIdAsync(id);
            if (offer == null)
            {
                throw new OfferNotFoundException("Offer not found");
            }

            return _mapper.Map<OfferResponse>(offer);
        }

        public async Task CreateOfferAsync(CreateOfferRequest createRequest)
        {
            var offer = _mapper.Map<Offer>(createRequest);

            if (createRequest.PackageIds != null)
            {
                offer.Packages = new List<Package>();

                foreach (var packageId in createRequest.PackageIds)
                {
                    try
                    {
                        var package = await _packageRepository.FindByIdAsync(packageId);
                        if (package != null)
                        {
                            offer.Packages.Add(package);
                        }
                        else
                        {
                            throw new PackageNotFoundException("Package not found.");
                        }
                    }
                    catch(InvalidIdException ex)
                    {
                        throw new PackageNotFoundException(ex.Message);
                    }
                }
            }

            await _offerRepository.InsertOneAsync(offer);
        }

        public async Task UpdateOfferAsync(string id, UpdateOfferRequest updateRequest)
        {
            var existingOffer = await _offerRepository.FindByIdAsync(id);
            if (existingOffer == null)
            {
                throw new OfferNotFoundException("Offer not found");
            }

            // Retrieve the existing packages associated with the offer
            var existingPackages = existingOffer.Packages;

            // Update the offer properties with the values from the update request
            _mapper.Map(updateRequest, existingOffer);

            // Update the package IDs of the offer with the new values from the update request
            existingOffer.Packages = new List<Package>();

            if (updateRequest.PackageIds != null)
            {
                foreach (var packageId in updateRequest.PackageIds)
                {
                    var package = await _packageRepository.FindByIdAsync(packageId);
                    if (package != null)
                    {
                        existingOffer.Packages.Add(package);
                    }
                }
            }

            await _offerRepository.ReplaceOneAsync(existingOffer);
        }

        public async Task DeleteOfferAsync(string id)
        {
            await _offerRepository.DeleteByIdAsync(id);
        }
    }
}

