using System.Security.Claims;

namespace Aqua_Sharp_Backend.Interfaces
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}