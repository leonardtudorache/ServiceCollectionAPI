using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Models.Enums;

namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> AddRole(Role role);
        Task<Role> GetRoleById(string id);
        Task<Role> UpdatePermissions(string id, List<Permission> permissions);
        Task<IEnumerable<Role>> GetAllRoles();
    }
}