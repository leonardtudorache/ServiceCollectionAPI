using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ServiceRequest;
using ServiceCollectionAPI.Models;

public interface IBusinessService
{
    Task AddService(CreateServiceRequest service);
    Task<BusinessServiceResponse> GetServiceById(string id);
    Task<List<BusinessServiceResponse>> GetAllServices();
    Task UpdateService(UpdateServiceRequest service);
    Task DeleteService(string id);
    Task AddEmployeesToService(List<Employee> employees, string serviceId);
}