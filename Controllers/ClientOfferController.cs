using ServiceCollectionAPI.Controllers.RequestModels.Request.ClientOffer;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAuthorization")]
    [ApiController]
    [Route("api/clientoffers")]
    public class ClientOfferController : ControllerBase
    {
        private readonly IClientOfferService _clientOfferService;
        private readonly ITenantContextService _tenantContextService;

        public ClientOfferController(IClientOfferService clientOfferService, ITenantContextService tenantContextService)
        {
            _clientOfferService = clientOfferService;
            _tenantContextService = tenantContextService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientOffers()
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

                var clientOffers = await _clientOfferService.GetAllClientOffersAsync();

                return Ok(clientOffers);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientOfferById(string id)
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

                var clientOffer = await _clientOfferService.GetClientOfferByIdAsync(id);

                return Ok(clientOffer);
            }
            catch (ClientOfferNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClientOffer(CreateClientOfferRequest createRequest)
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

                await _clientOfferService.CreateClientOfferAsync(createRequest);

                return Ok();
            }
            catch (OfferNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClientOffer(string id, UpdateClientOfferRequest updateRequest)
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

                await _clientOfferService.UpdateClientOfferAsync(id, updateRequest);

                return Ok();
            }
            catch (ClientOfferNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ClientNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (OfferNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientOffer(string id)
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

                await _clientOfferService.DeleteClientOfferAsync(id);

                return Ok();
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}

