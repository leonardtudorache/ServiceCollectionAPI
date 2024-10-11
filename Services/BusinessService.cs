using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ServiceRequest;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;

namespace ServiceCollectionAPI.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IMongoRepository<BusinessServiceModel> _businessServiceRepository;
        private readonly IMapper _mapper;

        public BusinessService(IMongoRepository<BusinessServiceModel> businessServiceRepository, IMapper mapper)
        {
            _businessServiceRepository = businessServiceRepository;
            _mapper = mapper;
        }
        public async Task AddEmployeesToService(List<Employee> employees, string serviceId)
        {
            BusinessServiceModel businessService=await _businessServiceRepository.FindByIdAsync(serviceId);

            if (businessService == null)
                throw new ArgumentException("Business doesn't exist");

            businessService.Employees.AddRange(employees);

            await _businessServiceRepository.ReplaceOneAsync(businessService);

        }

        public async Task AddService(CreateServiceRequest service)
        {
            BusinessServiceModel businessToAdd = _mapper.Map<BusinessServiceModel>(service);
            await _businessServiceRepository.InsertOneAsync(businessToAdd);
        }

        public async Task DeleteService(string id)
        {
            var business = await _businessServiceRepository.FindByIdAsync(id);

            if (business == null)
                throw new ArgumentException("Business not found");

            await _businessServiceRepository.DeleteByIdAsync(id);
        }

        public async Task<List<BusinessServiceResponse>> GetAllServices()
        {
            var businessServices = await _businessServiceRepository.FilterByAsync(e => true);
            return _mapper.Map<List<BusinessServiceResponse>>(businessServices);
        }

        public async Task<BusinessServiceResponse> GetServiceById(string id)
        {
            var business = await _businessServiceRepository.FindByIdAsync(id);

            if (business == null)
            {
                throw new ArgumentException("Business with id " + id + "doesn't exist");
            }

            return _mapper.Map<BusinessServiceResponse>(business);
        }

        public async Task UpdateService(UpdateServiceRequest service)
        {
            var business = _mapper.Map<BusinessServiceModel>(service);

            var businessToUpdate = await _businessServiceRepository.FindOneAsync(b => b.Id == business.Id);

            if (businessToUpdate == null)
                throw new ArgumentException("Business not found");

            businessToUpdate.Name = business.Name;
            businessToUpdate.Description = business.Description;
            businessToUpdate.Price = business.Price;
            businessToUpdate.ServiceType= business.ServiceType;
            businessToUpdate.Employees= business.Employees;


            await _businessServiceRepository.ReplaceOneAsync(businessToUpdate);
        }
    }
}
