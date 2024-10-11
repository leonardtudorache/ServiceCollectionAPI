using Model = ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Models;

namespace ServiceCollectionAPI.Controllers.RequestModels.Request.ClientOffer
{
	public class UpdateClientOfferRequest
    {
        public Status Status { get; set; }
        public string ClientId { get; set; } = "";
        public string OfferId { get; set; } = "";
    }
}

