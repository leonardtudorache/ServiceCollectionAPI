using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.ClientOffer;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces;

namespace ServiceCollectionAPI.Services
{
    public class ClientOfferService : IClientOfferService
    {
        private readonly IMongoRepository<ClientOffer> _clientOfferRepository;
        private readonly IMongoRepository<Client> _clientRepository;
        private readonly IMongoRepository<Offer> _offerRepository;
        private readonly IMapper _mapper;

        public ClientOfferService(IMongoRepository<ClientOffer> clientOfferRepository,
            IMongoRepository<Client> clientRepository,
            IMongoRepository<Offer> offerRepository,
            IMapper mapper)
        {
            _clientOfferRepository = clientOfferRepository;
            _clientRepository = clientRepository;
            _offerRepository = offerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientOfferResponse>> GetAllClientOffersAsync()
        {
            var clientOffers = await _clientOfferRepository.FilterByAsync(co => true);
            return _mapper.Map<IEnumerable<ClientOfferResponse>>(clientOffers);
        }

        public async Task<ClientOfferResponse> GetClientOfferByIdAsync(string id)
        {
            var clientOffer = await _clientOfferRepository.FindByIdAsync(id);
            return _mapper.Map<ClientOfferResponse>(clientOffer);
        }

        public async Task CreateClientOfferAsync(CreateClientOfferRequest createRequest)
        {

            var offer = await _offerRepository.FindByIdAsync(createRequest.OfferId);
            if (offer == null)
            {
                throw new OfferNotFoundException("Offer not found");
            }

            var client = _mapper.Map<Client>(createRequest.Client);
            await _clientRepository.InsertOneAsync(client);

            var clientOffer = new ClientOffer
            {
                Status = createRequest.Status,
                Client = client,
                Offer = offer
            };

            await _clientOfferRepository.InsertOneAsync(clientOffer);
        }

        public async Task UpdateClientOfferAsync(string id, UpdateClientOfferRequest updateRequest)
        {
            var existingClientOffer = await _clientOfferRepository.FindByIdAsync(id);
            if (existingClientOffer == null)
            {
                throw new ClientOfferNotFoundException("ClientOffer not found");
            }

            existingClientOffer.Status = updateRequest.Status;

            if (updateRequest.ClientId != null)
            {
                try
                {
                    var client = await _clientRepository.FindByIdAsync(updateRequest.ClientId);
                    if (client == null)
                    {
                        throw new ClientNotFoundException("Client not found");
                    }

                    existingClientOffer.Client = client;
                }
                catch(InvalidIdException ex)
                {
                    throw new ClientNotFoundException(ex.Message);
                }
            }

            if (updateRequest.OfferId != null)
            {
                try
                {
                    var offer = await _offerRepository.FindByIdAsync(updateRequest.OfferId);
                    if (offer == null)
                    {
                        throw new OfferNotFoundException("Offer not found");
                    }

                    existingClientOffer.Offer = offer;
                }
                catch (InvalidIdException ex)
                {
                    throw new ClientNotFoundException(ex.Message);
                }
            }

            await _clientOfferRepository.ReplaceOneAsync(existingClientOffer);
        }

        public async Task DeleteClientOfferAsync(string id)
        {
            await _clientOfferRepository.DeleteByIdAsync(id);
        }
    }
}