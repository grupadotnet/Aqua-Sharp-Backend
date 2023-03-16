﻿using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
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
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var aquarium = await _aquariumService.Get(id);
            
            var aquariumViewModel = _mapper.Map<AquariumViewModel>(aquarium);
            return Ok(aquariumViewModel);
        }
    }
}
