namespace ServiceCollectionAPI.Controllers.RequestModels.Request.Offer
{
    public class CreateOfferRequest
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public List<string> PackageIds { get; set; } = new List<string>();
    }
}

