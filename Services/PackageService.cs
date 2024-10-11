using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Request.PackageRquests;
using ServiceCollectionAPI.Controllers.RequestModels.Response;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces;

namespace ServiceCollectionAPI.Services
{
    public class PackageService : IPackageService
    {
        private readonly IMongoRepository<Package> _packageRepository;
        private readonly IMongoRepository<Product> _productRepository;
        private readonly IMongoRepository<BusinessServiceModel> _serviceRepository;
        private readonly IMapper _mapper;

        public PackageService(IMongoRepository<Package> packageRepository, IMongoRepository<Product> productRepository,
            IMongoRepository<BusinessServiceModel> serviceRepository,  IMapper mapper)
        {
            _packageRepository = packageRepository;
            _productRepository = productRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PackageResponse>> GetAllPackagesAsync()
        {
            var packages = await _packageRepository.FilterByAsync(p => true);
            return _mapper.Map<IEnumerable<PackageResponse>>(packages);
        }

        public async Task<PackageResponse> GetPackageByIdAsync(string id)
        {
            var package = await _packageRepository.FindByIdAsync(id);
            if (package == null)
            {
                throw new PackageNotFoundException("Package not found");
            }

            return _mapper.Map<PackageResponse>(package);
        }

        public async Task CreatePackageAsync(CreatePackageRequest createRequest)
        {
            var package = _mapper.Map<Package>(createRequest);

            if (createRequest.ProductIds != null)
            {
                package.Products = new List<Product>();

                foreach (var productId in createRequest.ProductIds)
                {
                    try
                    {
                        var product = await _productRepository.FindByIdAsync(productId);
                        if (product != null)
                        {
                            package.Products.Add(product);
                        }
                    }
                    catch (InvalidIdException ex)
                    {
                        throw new ProductNotFoundException(ex.Message);
                    }
                }
            }

            if (createRequest.ServiceIds != null)
            {
                package.Services = new List<BusinessServiceModel>();

                foreach (var serviceId in createRequest.ServiceIds)
                {
                    try
                    {
                        var service = await _serviceRepository.FindByIdAsync(serviceId);
                        if (service != null)
                        {
                            package.Services.Add(service);
                        }
                    }
                    catch(InvalidIdException ex)
                    {
                        throw new BusinessServiceNotFoundException(ex.Message);
                    }
                }
            }

            await _packageRepository.InsertOneAsync(package);
        }

        public async Task UpdatePackageAsync(string id, UpdatePackageRequest updateRequest)
        {
            var existingPackage = await _packageRepository.FindByIdAsync(id);
            if (existingPackage == null)
            {
                throw new PackageNotFoundException("Package not found");
            }

            _mapper.Map(updateRequest, existingPackage);

            if (updateRequest.ProductIds != null)
            {
                existingPackage.Products = new List<Product>();

                foreach (var productId in updateRequest.ProductIds)
                {
                    var product = await _productRepository.FindByIdAsync(productId);
                    if (product != null)
                    {
                        existingPackage.Products.Add(product);
                    }
                }
            }

            if (updateRequest.ServiceIds != null)
            {
                existingPackage.Services = new List<BusinessServiceModel>();

                foreach (var serviceId in updateRequest.ServiceIds)
                {
                    var service = await _serviceRepository.FindByIdAsync(serviceId);
                    if (service != null)
                    {
                        existingPackage.Services.Add(service);
                    }
                }
            }

            await _packageRepository.ReplaceOneAsync(existingPackage);
        }

        public async Task DeletePackageAsync(string id)
        {
            await _packageRepository.DeleteByIdAsync(id);
        }
    }
}

