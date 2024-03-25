using System.Net.Sockets;
using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels.Aquarium;
using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] JsonPatchDocument<EditAquariumViewModel> deviceModel)
        {
            await _deviceService.Update(id, deviceModel);
            return Ok();
        }

        //[HttpPatch("{id}/mode")]
        //public async Task<IActionResult> SwitchMode(int id, [FromQuery] bool manual)
        //{
        //    await _deviceService.SwitchMode(id, manual);
        //    return Ok();
        //}

        //[HttpPatch("{id}/lights")]
        //public async Task<IActionResult> SwitchLights(int id, [FromQuery] bool lightsOn)
        //{
        //    await _deviceService.SwitchLights(id, lightsOn);
        //    return Ok();
        //}
    }
}
