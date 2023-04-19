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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var device = await _deviceService.Get(id);

            return Ok(device);
        }
        
        [HttpGet("{id}/config")]
        public async Task<IActionResult> GetConfig(int id)
        {
            var config = await _deviceService.GetDeviceConfig(id);

            return Ok(config);
        }
    }
}
