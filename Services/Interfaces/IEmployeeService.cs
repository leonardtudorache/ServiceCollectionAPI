
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.EmployeeRequests;

namespace ServiceCollectionAPI.Services.Interfaces.UserManagement
{
    public interface IEmployeeService
    {
        Task AddEmployee(CreateEmployeeRequest createEmployeeRequest);
        Task<EmployeeResponse> GetEmployeeById(string employeeId);
        Task<List<EmployeeResponse>> GetAllEmployees();
        Task UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest);
        Task RemoveEmployee(string employeeId);
    }
}
