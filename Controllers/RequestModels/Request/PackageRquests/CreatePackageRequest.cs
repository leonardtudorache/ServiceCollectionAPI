namespace ServiceCollectionAPI.Controllers.RequestModels.Request.PackageRquests
{
    public class CreatePackageRequest
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string? PackageType { get; set; }
        public List<string>? ProductIds { get; set; }
        public List<string>? ServiceIds { get; set; }
    }
}

