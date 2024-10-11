using ServiceCollectionAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.TenantRequests;
using ServiceCollectionAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace ServiceCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAuthorization")]
    [Route("api/[controller]")]
    [ApiController]
    //difference between controller and controllerBase???
    public class TenantController : Controller
    {
        private readonly ITenantService _tenantService;
        private readonly ITenantContextService _tenantContextService;

        public TenantController(ITenantService tenantService, ITenantContextService tenantContextService)
        {
            _tenantService = tenantService;
            _tenantContextService = tenantContextService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenantAsync([FromBody] CreateTenantRequest createTenantRequest)
        {
            await _tenantService.InsertOneAsync(createTenantRequest);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTenantAsync()
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

                var tenant = await _tenantService.GetTenantJson(tenantId);
                return Ok(tenant);
            }
            catch (TenantNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> UpdateTenantAsync([FromBody] EditTenantRequest editTenantRequest)
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

                await _tenantService.UpdateTenant(editTenantRequest);

                return Ok();
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
