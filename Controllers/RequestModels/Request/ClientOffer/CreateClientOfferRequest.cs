using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Controllers.RequestModels.Request.Client;

namespace ServiceCollectionAPI.Controllers.RequestModels.Request.ClientOffer
{
	public class CreateClientOfferRequest
    {
        public Status Status { get; set; }
        public CreateClientRequest Client { get; set; } = null!;
        public string OfferId { get; set; } = "";
    }
}

