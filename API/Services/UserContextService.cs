﻿using System.Security.Claims;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Interfaces;

namespace Aqua_Sharp_Backend.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public int? GetUserId => User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public string? GetUserRole => User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
    }
}
