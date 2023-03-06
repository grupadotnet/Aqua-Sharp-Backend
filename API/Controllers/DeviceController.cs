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

        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] CreateDeviceViewModel createDeviceViewModel)
        {
            var device = await _deviceService.Add(createDeviceViewModel);

            return Ok(device);
        }
    }
}
