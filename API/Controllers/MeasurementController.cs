using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels.Measurement;

namespace Aqua_Sharp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService; 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMeasurementViewModel viewModel)
        {
            var res = await _measurementService.Create(viewModel);
            return Ok(res);
        }
    }
}
