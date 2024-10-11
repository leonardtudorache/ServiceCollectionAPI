using ServiceCollectionAPI.Controllers.RequestModels.Request.Client;
using ServiceCollectionAPI.Controllers.RequestModels.Response;

namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientResponse>> GetAllClientsAsync();
        Task<ClientResponse> GetClientByIdAsync(string id);
        Task CreateClientAsync(CreateClientRequest createRequest);
        Task UpdateClientAsync(string id, UpdateClientRequest updateRequest);
        Task DeleteClientAsync(string id);
    }
}

