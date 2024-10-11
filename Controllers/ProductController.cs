using Microsoft.AspNetCore.Mvc;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Services.Interfaces;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.ProductRequest;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ServiceCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAuthorization")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ITenantContextService _tenantContextService;

        public ProductController(IProductService productService, ITenantContextService tenantContextService)
        {
            _productService = productService;
            _tenantContextService = tenantContextService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest product)
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

                await _productService.AddProduct(product);

                return Ok();
            }
            catch (ProductAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetProductById/{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(string productId)
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

                var product = await _productService.GetProductById(productId);

                return Ok(product);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProductsAsync()
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

                List<ProductResponse> products = new List<ProductResponse>();
                products = await _productService.GetAllProducts();

                if (products == null || products.Count == 0)
                    return NotFound("No products have been found");

                return Ok(products);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductRequest updateProductRequest)
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

                await _productService.UpdateProduct(updateProductRequest);

                return Ok();
            }
            catch (ProductNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Remove/{productId}")]
        public async Task<IActionResult> RemoveProductAsync(string productId)
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

                await _productService.DeleteProduct(productId);

                return Ok();
            }
            catch (ProductNotFoundException ex)
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
