using System.Net.Sockets;
using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService; 
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var device = await _deviceService.Get(id);

            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateDeviceViewModel createDeviceViewModel)
        {
            var device = await _deviceService.Add(createDeviceViewModel);

            return Ok(device);
        }
    }
}
