using ServiceCollectionAPI.Controllers.RequestModels.Request.Contract;
using ServiceCollectionAPI.Controllers.RequestModels.Response;

namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<ContractResponse>> GetAllContractsAsync();
        Task<ContractResponse> GetContractByIdAsync(string id);
        Task CreateContractAsync(CreateContractRequest createRequest);
        Task UpdateContractAsync(string id, UpdateContractRequest updateRequest);
        Task DeleteContractAsync(string id);
    }
}

