using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Models.Enums;
using ServiceCollectionAPI.Repositories.Interfaces;


namespace ServiceCollectionAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMongoRepository<Role> _roleRepository;

        public RoleService(IMongoRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> AddRole(Role role)
        {
            await _roleRepository.InsertOneAsync(role);
            return role;
        }

        public async Task<Role> GetRoleById(string id)
        {
            var role = await _roleRepository.FindByIdAsync(id);

            if (role == null)
            {
                throw new ArgumentException("Role not found");
            }

            return role;
        }

        public async Task<Role> UpdateRolePermissions(string id, List<Permission> permissions)
        {
            var role = await GetRoleById(id);

            role.Permissions = permissions;
            await _roleRepository.ReplaceOneAsync(role);

            return role;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _roleRepository.FilterByAsync(r => true);
            return roles.ToList();
        }
    }
}