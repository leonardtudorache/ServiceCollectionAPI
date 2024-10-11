using Microsoft.AspNetCore.Mvc;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Services.Interfaces;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using Microsoft.AspNetCore.Authorization;
using ServiceCollectionAPI.Services.Interfaces.UserManagement;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.EmployeeRequests;

namespace ServiceCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAuthorization")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITenantContextService _tenantContextService;

        public EmployeeController(IEmployeeService employeeService, ITenantContextService tenantContextService)
        {
            _employeeService = employeeService;
            _tenantContextService = tenantContextService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeRequest employee)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                await _employeeService.AddEmployee(employee);

                return Ok();
            }
            catch (EmployeeAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEmployeeById/{employeeId}")]
        public async Task<IActionResult> GetProductByIdAsync(string employeeId)
        {

            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                var employee = await _employeeService.GetEmployeeById(employeeId);

                return Ok(employee);
            }
            catch (EmployeeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetAllEmployeesAsync()
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                List<EmployeeResponse> employees = new List<EmployeeResponse>();
                employees = await _employeeService.GetAllEmployees();

                if (employees == null || employees.Count == 0)
                    return NotFound("No employees have been found");

                return Ok(employees);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromBody] UpdateEmployeeRequest employee)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                await _employeeService.UpdateEmployee(employee);

                return Ok();
            }
            catch (EmployeeNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveEmployee/{employeeId}")]
        public async Task<IActionResult> RemoveProductAsync(string employeeId)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                await _employeeService.RemoveEmployee(employeeId);

                return Ok();
            }
            catch (EmployeeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
