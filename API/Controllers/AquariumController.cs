using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aqua_Sharp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AquariumController : ControllerBase
    {
        private readonly IAquariumService _aquariumService;
        public AquariumController(IAquariumService aquariumService)
        {
            _aquariumService = aquariumService;

        }
    }
}
