﻿using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels.Aquarium;

namespace Aqua_Sharp_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var aquarium = await _aquariumService.GetOne(id);

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

    }
}
