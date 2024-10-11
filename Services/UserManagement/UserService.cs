using AutoMapper;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services.Interfaces.UserManagement;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.UserRequests;
using MongoDB.Driver;
using ServiceCollectionAPI.Models;
using ServiceCollectionAPI.Models.Enums;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Services.Interfaces;

namespace ServiceCollectionAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IRoleService _roleService;
        private readonly ITenantContextService _tenantContextService;

        public UserService(IMongoRepository<User> userRepo, IMapper mapper,
        ILogger<UserService> logger, ITenantService tenantService, IRoleService roleService, ITenantContextService tenantContextService)
        {
            _userRepository = userRepo;
            _mapper = mapper;
            _tenantService = tenantService;
            _roleService = roleService;
            _tenantContextService = tenantContextService;
        }

        public async Task AddUser(CreateUserRequest createUserRequest)
        {
            List<Role> roles = new List<Role>();
            User userToInsert = _mapper.Map<User>(createUserRequest);
            Tenant tenant = await _tenantService.GetTenant(_tenantContextService.GetTenantId());
            userToInsert.TenantId = tenant.Id;

            // To test this, probably throws a null reference exception
            var user = await _userRepository.FindOneAsync(u => u.Phone == userToInsert.Phone);
            if (user != null && user.TenantRoles.Any(t => t.Key == tenant.Id.ToString()))
            {
                throw new UserAlreadyExistsException("There is already a user with that phone.");
            }

            foreach (var roleId in createUserRequest.RoleIds!)
            {
                var role = await _roleService.GetRoleById(roleId);
                roles.Add(role);
            }

            // Check if user exists
            if (user != null)
            {
                // Add new tenant and roles to existing user
                user.TenantRoles.Add(tenant.Id.ToString(), roles);
                await _userRepository.ReplaceOneAsync(user);
            }
            else
            {
                //Create new user
                userToInsert.TenantRoles.Add(tenant.Id.ToString(), roles);
                await _userRepository.InsertOneAsync(userToInsert);
            }

        }

        public async Task AddUserWithNewTenant(CreateUserRequest createUserRequest)
        {
            Tenant tenant = new Tenant();
            User userToInsert = _mapper.Map<User>(createUserRequest);

            tenant = await _tenantService.InsertEmptyTenant();

            _tenantContextService.SetTenantId(tenant.Id.ToString());

            // Create Admin role by default for a new tenant and assign to the new user
            List<Permission> adminPermissions = new List<Permission>();
            Array permissionValues = Enum.GetValues(typeof(Permission));
            foreach (Permission permission in permissionValues)
            {
                adminPermissions.Add(permission);
            }
            Role adminRole = new Role { Name = "Admin", Permissions = adminPermissions, TenantId = tenant.Id };
            await _roleService.AddRole(adminRole);

            List<Role> roles = new List<Role>() { adminRole };

            // Check if user exists
            var user = await _userRepository.FindOneAsync(u => u.Phone == userToInsert.Phone);
            if (user != null)
            {
                // Add new tenant and roles to existing user
                user.TenantRoles.Add(tenant.Id.ToString(), roles);
                await _userRepository.ReplaceOneAsync(user);
            }
            else
            {
                //Create new user
                userToInsert.TenantRoles.Add(tenant.Id.ToString(), roles);
                await _userRepository.InsertOneAsync(userToInsert);
            }
        }

        public bool VerifyPermission(User user, Tenant tenant, Permission permissionRequired)
        {
            if (!user.TenantRoles.ContainsKey(tenant.Id.ToString()))
            {
                return false;
            }

            List<Role> roles = user.TenantRoles[tenant.Id.ToString()];

            foreach (var role in roles)
            {
                if (role.Permissions.Exists(p => p == permissionRequired))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<UserResponse> GetUserByEmail(string email)
        {
            var user = await _userRepository.FindOneAsync(u => u.Email == email);
            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> GetUserByPhone(string phone)
        {
            var user = await _userRepository.FindOneAsync(u => u.Phone == phone, true);
            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> GetUserById(string userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = await _userRepository.FilterByAsync(u => true);

            return _mapper.Map<List<UserResponse>>(users);
        }

        public async Task UpdateUser(UpdateUserRequest updateUserRequest)
        {
            var user = _mapper.Map<User>(updateUserRequest);
            var userToUpdate = await _userRepository.FindOneAsync(u => u.Phone == user.Phone);

            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            userToUpdate.Email = user.Email; ;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Phone = user.Phone;
            userToUpdate.ProfilePictureUrl = user.ProfilePictureUrl;
            userToUpdate.Role = user.Role;

            await _userRepository.ReplaceOneAsync(userToUpdate);
        }

        public async Task BlockUser(string userId)
        {
            var userToUpdate = await _userRepository.FindByIdAsync(userId);

            if (userToUpdate == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            userToUpdate.IsBlocked = true;

            await _userRepository.ReplaceOneAsync(userToUpdate);
        }

        public async Task UnblockUser(string userId)
        {
            var userToUpdate = await _userRepository.FindByIdAsync(userId);

            if (userToUpdate == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            userToUpdate.IsBlocked = false;

            await _userRepository.ReplaceOneAsync(userToUpdate);
        }

        public async Task RemoveUser(string userId)
        {
            var userToRemove = await _userRepository.FindByIdAsync(userId);

            if (userToRemove == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            userToRemove.TenantRoles.Remove(_tenantContextService.GetTenantId());

            await _userRepository.ReplaceOneAsync(userToRemove);
        }
    }
}
