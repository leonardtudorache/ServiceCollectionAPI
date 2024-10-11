using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers.RequestModels.Response
{
	public class OfferResponse
	{
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public List<PackageResponse> Packages { get; set; } = new List<PackageResponse>();
    }
}

