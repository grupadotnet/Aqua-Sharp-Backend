using Aqua_Sharp_Backend.Interfaces;
using Aqua_Sharp_Backend.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.ViewModels.Measurement;

namespace Aqua_Sharp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;
        
        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService; 
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Get([FromQuery] GetMeasurementsPaginationViewModel paginationViewModel)
        {
            var res = await _measurementService.Get(paginationViewModel);

            return Ok(res);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetMeasurementsStartFromViewModel viewModel)
        {
            var res = await _measurementService.Get(viewModel);

            return Ok(res);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _measurementService.Delete(id);

            return NoContent();
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> Index([FromQuery] int userId)
        {
            _measurementService.SendMes(userId);
            return Ok();
        }
    }
}
