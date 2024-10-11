namespace ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.UserRequests
{
    public class CreateUserRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Role { get; set; } = "User";
        public bool IsBlocked { get; set; } = false;
        public List<string>? RoleIds { get; set; }
    }
}