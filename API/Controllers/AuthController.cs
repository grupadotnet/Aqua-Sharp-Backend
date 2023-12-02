using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels.Config;

namespace Aqua_Sharp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfigService _configService;

        public AuthController(IConfigService configService)
        {
            _configService = configService; 
        }
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] LoginViewModel dto)
        {
            string token = _configService.GenerateJwt(dto);
            return Ok(token);

        }
    }
}
