using ServiceCollectionAPI.Controllers.RequestModels.Request.PackageRquests;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAuthorization")]
    [ApiController]
    [Route("api/packages")]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;
        private readonly ITenantContextService _tenantContextService;

        public PackageController(IPackageService packageService, ITenantContextService tenantContextService)
        {
            _packageService = packageService;
            _tenantContextService = tenantContextService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPackages()
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

                var packages = await _packageService.GetAllPackagesAsync();

                return Ok(packages);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(string id)
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

                var package = await _packageService.GetPackageByIdAsync(id);

                return Ok(package);
            }
            catch (PackageNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackage(CreatePackageRequest createRequest)
        {
            #region SetTenantId
            HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
            if (tenantId.Count == 0)
            {
                throw new TenantIdNotSetException("Tenant not set.");
            }
            _tenantContextService.SetTenantId(tenantId.First());
            #endregion

            await _packageService.CreatePackageAsync(createRequest);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackage(string id, UpdatePackageRequest updateRequest)
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

                await _packageService.UpdatePackageAsync(id, updateRequest);

                return Ok();
            }
            catch (PackageNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(string id)
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

                await _packageService.DeletePackageAsync(id);

                return Ok();
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

