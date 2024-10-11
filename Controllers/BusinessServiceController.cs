using Microsoft.AspNetCore.Mvc;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Services.Interfaces;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ServiceRequest;
using Microsoft.AspNetCore.Authorization;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAuthorization")]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessServiceController : Controller
    {
        private readonly IBusinessService _businessService;
        private readonly ITenantContextService _tenantContextService;

        public BusinessServiceController(IBusinessService businessService, ITenantContextService tenantContextService)
        {
            _businessService = businessService;
            _tenantContextService = tenantContextService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateServiceAsync([FromBody] CreateServiceRequest service)
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

                await _businessService.AddService(service);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetServiceById/{serviceId}")]
        public async Task<IActionResult> GetServiceByIdAsync(string serviceId)
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

                var service = await _businessService.GetServiceById(serviceId);

                return Ok(service);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<BusinessServiceResponse>>> GetAllServicesAsync()
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

                List<BusinessServiceResponse> services = await _businessService.GetAllServices();

                if (services == null || services.Count == 0)
                    return NotFound("No services have been found");

                return Ok(services);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateServiceAsync([FromBody] UpdateServiceRequest service)
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

                await _businessService.UpdateService(service);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveService/{serviceId}")]
        public async Task<IActionResult> RemoveServiceAsync(string serviceId)
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

                await _businessService.DeleteService(serviceId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddEmployees/{serviceId}")]
        public async Task<IActionResult> AddEmployeesToServiceAsync(string serviceId, [FromBody] List<Employee> employees)
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

                await _businessService.AddEmployeesToService(employees, serviceId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
