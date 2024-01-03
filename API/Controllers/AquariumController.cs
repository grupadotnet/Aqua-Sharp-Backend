using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels.Aquarium;

using Microsoft.AspNetCore.Authorization;

namespace Aqua_Sharp_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AquariumController : ControllerBase
    {
        private readonly IAquariumService _aquariumService;
        private readonly IMapper _mapper;

        public AquariumController(IAquariumService aquariumService, IMapper mapper)
        {
            _aquariumService = aquariumService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Authorize(Roles ="al")]
        public async Task<IActionResult> GetAll()
        {
            var aquariumList = await _aquariumService.GetAll();

            var aquariumListViewModel = _mapper.Map<List<AquariumViewModel>>(aquariumList);
            return Ok(aquariumListViewModel);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var aquarium = await _aquariumService.Get(id);
            
            var aquariumViewModel = _mapper.Map<AquariumViewModel>(aquarium);
            return Ok(aquariumViewModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _aquariumService.Delete(id);
            
            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAquariumViewModel createAquariumViewModel)
        {
            var aquarium = await _aquariumService.Add(createAquariumViewModel);
            
            var aquariumViewModel = _mapper.Map<AquariumViewModel>(aquarium);
            return Ok(aquariumViewModel);
        }
    }
}
