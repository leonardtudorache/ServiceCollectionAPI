using ServiceCollectionAPI.Controllers.RequestModels.Request.ClientOffer;
using ServiceCollectionAPI.Controllers.RequestModels.Response;

namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface IClientOfferService
    {
        Task<IEnumerable<ClientOfferResponse>> GetAllClientOffersAsync();
        Task<ClientOfferResponse> GetClientOfferByIdAsync(string id);
        Task CreateClientOfferAsync(CreateClientOfferRequest createRequest);
        Task UpdateClientOfferAsync(string id, UpdateClientOfferRequest updateRequest);
        Task DeleteClientOfferAsync(string id);
    }
}

