namespace ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.TenantRequests
{
    public class CreateTenantRequest
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? IBAN { get; set; } = "";
        public string CUI { get; set; } = null!;
        public string RegComert { get; set; } = null!;
        public string City { get; set; } = null!;
        public string County { get; set; } = null!;
        public string? Phone { get; set; }
    }
}