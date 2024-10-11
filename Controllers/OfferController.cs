using System;
using ServiceCollectionAPI.Controllers.RequestModels.Request.Offer;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAuthorization")]
    [ApiController]
    [Route("api/offers")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly ITenantContextService _tenantContextService;

        public OfferController(IOfferService offerService, ITenantContextService tenantContextService)
        {
            _offerService = offerService;
            _tenantContextService = tenantContextService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOffers()
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

                var offers = await _offerService.GetAllOffersAsync();

                return Ok(offers);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferById(string id)
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

                var offer = await _offerService.GetOfferByIdAsync(id);

                return Ok(offer);
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

        [HttpPost]
        public async Task<IActionResult> CreateOffer(CreateOfferRequest createRequest)
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

                await _offerService.CreateOfferAsync(createRequest);
                return Ok();
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer(string id, UpdateOfferRequest updateRequest)
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

                await _offerService.UpdateOfferAsync(id, updateRequest);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(string id)
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

                await _offerService.DeleteOfferAsync(id);

                return Ok();
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

