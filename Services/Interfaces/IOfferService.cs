using ServiceCollectionAPI.Controllers.RequestModels.Request.Offer;
using ServiceCollectionAPI.Controllers.RequestModels.Response;

namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface IOfferService
    {
        Task<IEnumerable<OfferResponse>> GetAllOffersAsync();
        Task<OfferResponse> GetOfferByIdAsync(string id);
        Task CreateOfferAsync(CreateOfferRequest createRequest);
        Task UpdateOfferAsync(string id, UpdateOfferRequest updateRequest);
        Task DeleteOfferAsync(string id);
    }
}

