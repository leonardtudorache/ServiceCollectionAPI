using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.UserRequests;

namespace ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.EmployeeRequests
{
    public class UpdateEmployeeRequest: UpdateUserRequest
    {
        public string? Description { get; set; }
        public List<string> Links { get; set; } = new List<string>();
        public List<string> Photos { get; set; } = new List<string>();
    }
}