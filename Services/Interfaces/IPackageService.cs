using ServiceCollectionAPI.Controllers.RequestModels.Request.PackageRquests;
using ServiceCollectionAPI.Controllers.RequestModels.Response;

namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface IPackageService
    {
        Task<IEnumerable<PackageResponse>> GetAllPackagesAsync();
        Task<PackageResponse> GetPackageByIdAsync(string id);
        Task CreatePackageAsync(CreatePackageRequest createRequest);
        Task UpdatePackageAsync(string id, UpdatePackageRequest updateRequest);
        Task DeletePackageAsync(string id);
    }
}

