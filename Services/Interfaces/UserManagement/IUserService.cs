using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.UserRequests;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Models.Enums;

namespace ServiceCollectionAPI.Services.Interfaces.UserManagement
{
    public interface IUserService
    {
        Task AddUser(CreateUserRequest user);
        Task AddUserWithNewTenant(CreateUserRequest createUserRequest);
        bool VerifyPermission(User user, Tenant tenant, Permission permissionRequired);
        Task<UserResponse> GetUserByEmail(string email);
        Task<UserResponse> GetUserByPhone(string phone);
        Task<UserResponse> GetUserById(string userId);
        Task<List<UserResponse>> GetAllUsers();
        Task UpdateUser(UpdateUserRequest updateUserRequest);
        Task BlockUser(string userId);
        Task UnblockUser(string userId);
        Task RemoveUser(string userId);
    }
}
