using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.UserRequests;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.TenantRequests;

namespace ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.EmployeeRequests
{
    public class CreateEmployeeRequest : CreateUserRequest
    {
        public string? Description { get; set; }
        public List<string> Links { get; set; } = new List<string>();
        public List<string> Photos { get; set; } = new List<string>();
    }
}