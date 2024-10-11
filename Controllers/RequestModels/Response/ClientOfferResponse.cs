using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers.RequestModels.Response
{
	public class ClientOfferResponse
	{
        public string Id { get; set; } = "";
        public Status Status { get; set; }
        public ClientResponse Client { get; set; } = null!;
        public OfferResponse Offer { get; set; } = null!;
    }
}

