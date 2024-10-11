using ServiceCollectionAPI.Attributes;

namespace ServiceCollectionAPI.Models;

[BsonCollection("User")]
public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ProfilePictureUrl { get; set; }
    public string Role { get; set; } = null!;
    public bool IsBlocked { get; set; } = false;
    // Add role for user per tenant
    public Dictionary<string, List<Role>> TenantRoles { get; set; } = new Dictionary<string, List<Role>>();

}