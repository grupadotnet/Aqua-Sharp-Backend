using Microsoft.AspNetCore.Authorization;
using Models.Entities;
using System.Security.Claims;

namespace Aqua_Sharp_Backend.Authorization
{
    public class AquariumResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, List<Aquarium>>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, List<Aquarium> aquarium)
        {

            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (((userRole == ((int)RoleName.Own).ToString()) || (userRole == ((int)RoleName.All).ToString())) && (aquarium.All(a => a.UserId == int.Parse(userId))) && requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            if (userRole == ((int)RoleName.Own).ToString() && aquarium.All(a => a.UserId == int.Parse(userId)) && (requirement.ResourceOperation == ResourceOperation.Delete || requirement.ResourceOperation == ResourceOperation.Read))
            {
                context.Succeed(requirement);
            }
            if (userRole ==((int)RoleName.All).ToString() && (requirement.ResourceOperation == ResourceOperation.Delete || requirement.ResourceOperation == ResourceOperation.Read))
            {
                context.Succeed(requirement);
            }
            

            return Task.CompletedTask;
        }
    }
}
