using AutoMapper;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.EmployeeRequests;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces.UserManagement;

namespace ServiceCollectionAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IMongoRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;

        }
        public async Task AddEmployee(CreateEmployeeRequest createEmployeeRequest)
        {
            Employee employeeToAdd = _mapper.Map<Employee>(createEmployeeRequest);
            await _employeeRepository.InsertOneAsync(employeeToAdd);
        }

        public async Task<List<EmployeeResponse>> GetAllEmployees()
        {
            var employees = await _employeeRepository.FilterByAsync(e => true);
            return _mapper.Map<List<EmployeeResponse>>(employees);
        }

        public async Task<EmployeeResponse> GetEmployeeById(string employeeId)
        {
            var employee = await _employeeRepository.FindByIdAsync(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Employee with id " + employeeId + "doesn't exist");
            }

            return _mapper.Map<EmployeeResponse>(employee);
        }

        public async Task RemoveEmployee(string employeeId)
        {
            var employee = await _employeeRepository.FindByIdAsync(employeeId);

            if (employee == null)
                throw new ArgumentException("Employee not found");

            await _employeeRepository.DeleteByIdAsync(employeeId);
        }

        public async Task UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest)
        {
            var employee = _mapper.Map<Employee>(updateEmployeeRequest);

            var employeeToUpdate = await _employeeRepository.FindOneAsync(e => e.Id == employee.Id);

            if (employeeToUpdate == null)
                throw new ArgumentException("Employee not found");

            employeeToUpdate.Email = employee.Email; ;
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Phone = employee.Phone;
            employeeToUpdate.ProfilePictureUrl = employee.ProfilePictureUrl;
            employeeToUpdate.Role = employee.Role;
            employeeToUpdate.Description= employee.Description;
            employeeToUpdate.Links= employee.Links;
            employeeToUpdate.Photos= employee.Photos;


            await _employeeRepository.ReplaceOneAsync(employeeToUpdate);
        }
    }
}
