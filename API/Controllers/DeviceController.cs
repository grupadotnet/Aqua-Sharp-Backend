using System.Net.Sockets;
using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPatch("{id}/mode")]
        public async Task<IActionResult> SwitchMode(int id, [FromQuery] bool manual)
        {
            await _deviceService.SwitchMode(id, manual);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] JsonPatchDocument<Device> deviceUpdate)
        {
            var device = await _deviceService.Get(id);

            if (device == null)
            {
                return NotFound();
            }

            // create a temporary device with only the MeasurementFrequency property
            var deviceToPatch = new Device
            {
                MeasurementFrequency = device.MeasurementFrequency
            };

            // apply the JsonPatchDocument to the temporary device
            deviceUpdate.ApplyTo(deviceToPatch, ModelState);

            // check for any validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // update the device's MeasurementFrequency property
            device.MeasurementFrequency = deviceToPatch.MeasurementFrequency;

            // save the changes to the database
            await _deviceService.Update(device);

            return NoContent();
        }
    }
    }
