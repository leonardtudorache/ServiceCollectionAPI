
namespace ServiceCollectionAPI.Controllers.RequestModels.Generic
{
    public class UserResponse
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public string Role { get; set; } = null!;
        public bool IsBlocked { get; set; } = false;
        public Dictionary<string, List<RoleResponse>>? TenantRoles { get; set; } = new Dictionary<string, List<RoleResponse>>();

    }
}
