using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels.Config;

namespace Aqua_Sharp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IAuthService _configService;

        public ConfigController(IAuthService configService)
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
