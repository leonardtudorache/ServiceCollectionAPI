using ServiceCollectionAPI.Attributes;
using ServiceCollectionAPI.Models.Enums;

namespace ServiceCollectionAPI.Models;

[BsonCollection("Roles")]
public class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public List<Permission> Permissions { get; set; } = new List<Permission>();

}

