﻿using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Aqua_Sharp_Backend.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Aquarium>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Aquarium aquarium)
        {
            
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            
            if(userRole == "own" && aquarium.UserId == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            if(userRole =="all")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
