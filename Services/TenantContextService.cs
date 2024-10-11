using ServiceCollectionAPI.Services.Interfaces;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Exceptions;

namespace ServiceCollectionAPI.Services
{
    public class TenantContextService : ITenantContextService
    {
        private readonly ILogger<TenantService> _logger;
        private string TenantId = "";

        public TenantContextService(ILogger<TenantService> logger)
        {
            _logger = logger;
        }

        public void SetTenantId(string tenantId)
        {
            TenantId = tenantId;
        }

        // public async Task<Tenant> GetTenant()
        // {
        //     if (TenantId.Length < 0)
        //     {
        //         throw new TenantIdNotSetException("The tenant ID was not set int the requests.");
        //     }

        //     var tenant = await _tenantService.GetTenant(TenantId!);

        //     if (tenant == null)
        //     {
        //         throw new TenantNotFoundException("Tenant not found.");
        //     }

        //     return tenant;
        // }

        public string GetTenantId()
        {
            return TenantId;

        }
    }
}
