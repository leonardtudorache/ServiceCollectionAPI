using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers.RequestModels.Response
{
    public class PackageResponse
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string PackageType { get; set; } = "";
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
        public List<BusinessServiceModel> Services { get; set; } = new List<BusinessServiceModel>();
    }
}

