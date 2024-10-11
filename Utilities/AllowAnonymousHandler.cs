using Microsoft.AspNetCore.Authorization;

public class AllowAnonymousHandler : AuthorizationHandler<AllowAnonymousRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowAnonymousRequirement requirement)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}