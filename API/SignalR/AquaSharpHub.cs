using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Models.Entities;

namespace Aqua_Sharp_Backend.SignalR
{
    [Authorize]
    public class AquaSharpHub : Hub
    {
        
        public override async Task OnConnectedAsync()
        {
              await base.OnConnectedAsync();

        }
        
    }
}
