using ServiceCollectionAPI.Models.Enums;

namespace ServiceCollectionAPI.Controllers.RequestModels.Generic
{
    public class RoleResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<Permission> Permissions { get; set; } = new List<Permission>();

    }
}
