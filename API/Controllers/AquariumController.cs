using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aqua_Sharp_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AquariumController : ControllerBase
    {
        private readonly IAquariumService _aquariumService;
        public AquariumController(IAquariumService aquariumService)
        {
            _aquariumService = aquariumService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var aquariumViewModel = await _aquariumService.GetOne(id);
                if (aquariumViewModel == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                return Ok(aquariumViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
