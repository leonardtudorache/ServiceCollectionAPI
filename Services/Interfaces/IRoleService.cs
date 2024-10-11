using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Models.Enums;

public interface IRoleService
{
    Task<Role> AddRole(Role role);
    Task<Role> GetRoleById(string id);
    Task<Role> UpdateRolePermissions(string id, List<Permission> permissions);
    Task<List<Role>> GetAllRoles();
}