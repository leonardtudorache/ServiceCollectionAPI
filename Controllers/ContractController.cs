using ServiceCollectionAPI.Controllers.RequestModels.Request.Contract;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAuthorization")]
    [ApiController]
    [Route("api/contracts")]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly ITenantContextService _tenantContextService;

        public ContractController(IContractService contractService, ITenantContextService tenantContextService)
        {
            _contractService = contractService;
            _tenantContextService = tenantContextService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContracts()
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

                var contracts = await _contractService.GetAllContractsAsync();

                return Ok(contracts);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById(string id)
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

                var contract = await _contractService.GetContractByIdAsync(id);

                return Ok(contract);
            }
            catch (ContractNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract(CreateContractRequest createRequest)
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

                await _contractService.CreateContractAsync(createRequest);
                return Ok();
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(string id, UpdateContractRequest updateRequest)
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

                await _contractService.UpdateContractAsync(id, updateRequest);

                return Ok();
            }
            catch (ContractNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(string id)
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

                await _contractService.DeleteContractAsync(id);
                return Ok();
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

