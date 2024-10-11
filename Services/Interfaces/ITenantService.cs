using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.TenantRequests;
using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface ITenantService
    {
        Task<Tenant> InsertOneAsync(CreateTenantRequest tenant);
        Task<Tenant> InsertEmptyTenant();
        Task<Tenant> GetTenant(string tenantId);
        Task<TenantResponse> GetTenantJson(string tenantId);
        Task UpdateTenant(EditTenantRequest editTenantRequest);
    }
}
