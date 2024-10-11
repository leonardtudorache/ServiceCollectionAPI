namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface ITenantContextService
    {
        void SetTenantId(string tenantId);
        // Task<Tenant> GetTenant();
        string GetTenantId();

    }
}
