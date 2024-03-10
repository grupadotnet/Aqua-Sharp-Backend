using System.Text.Json;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Exceptions;
using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Models.ViewModels.Aquarium;
using Models.ViewModels.Device;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Models.DTO.MQTT;
using Boolean = System.Boolean;

namespace Aqua_Sharp_Backend.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IMqqtClientService _mqqtClient;

        public DeviceService(Context context, IMapper mapper, IMqqtClientService mqqtClient)
        {
            _context = context;
            _mapper = mapper;
            _mqqtClient = mqqtClient;
        }

        public async Task<bool> Add(CreateDeviceViewModel createDeviceViewModel)
        {
            if (createDeviceViewModel.Aquarium.Device != null)
                throw new BadRequest400Exception(
                    $"400. Aquarium with id: {createDeviceViewModel.Aquarium.AquariumId} already has a device!");

            var deviceToAdd = _mapper.Map<Device>(createDeviceViewModel);

            var addedDevice = await _context.Devices.AddAsync(deviceToAdd);
            await _context.SaveChangesAsync();

            return addedDevice.Entity != null;
        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<Device> Get(int id)
        {
            var device = await _context.Devices
                .AsNoTracking()
                .Include(d => d.Aquarium)
                .FirstOrDefaultAsync(d => d.DeviceId == id);

            if (device == null)
                throw new NotFound404Exception(
                    $"404. Aquarium with id: {id} not found!");

            return device;
        }

        public async Task<DeviceConfigViewModel> GetDeviceConfig(int id)
        {
            var device = await this.Get(id);

            return _mapper.Map<DeviceConfigViewModel>(device);

        }

        public Task<Device> Update()
        {
            throw new NotImplementedException();
        }
        public async Task Update(int id, JsonPatchDocument<EditAquariumViewModel> deviceModel)
        {
            var deviceToUpdate = await _context.Devices.Include(d => d.Aquarium).FirstOrDefaultAsync(d => d.DeviceId == id);

            if (deviceToUpdate != null)
            {
                var invalidProperties = deviceModel.Operations
                    .Select(o => o.path)
                    .Where(p => !IsValidProperty(p, typeof(EditAquariumViewModel)))
                    .ToList();

                if (invalidProperties.Any())
                {
                    throw new BadRequest400Exception($"Cannot modify properties: {string.Join(", ", invalidProperties)}!");

                }
                var editAquariumViewModel = _mapper.Map<EditAquariumViewModel>(deviceToUpdate.Aquarium);
                deviceModel.ApplyTo(editAquariumViewModel);
                _mapper.Map(editAquariumViewModel, deviceToUpdate.Aquarium);

                deviceToUpdate.MeasurementFrequency = editAquariumViewModel.MeasurementFrequency; 


                await _context.SaveChangesAsync();
            }

     
    }
        private bool IsValidProperty(string path, Type type)
        {
            var property = type.GetProperty(path, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            return property != null;
        }

        public async Task<bool> CheckIfDeviceExistsAsync(int id)
        {
            return await _context.Devices
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DeviceId == id) != null;
        }

        public async Task SwitchMode(int id, bool manual)
        {

            var device = await Get(id);
            device.ManualMode = manual;
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
            await SendMessage(id, manual, "mode");
        }
        public async Task SwitchLights(int id, bool lightsOn)
        {
            var device = await Get(id);

            if (device.ManualMode == false)
            {
                throw new BadRequest400Exception("Manual mode is turned off.");
            }

            await SendMessage(id, lightsOn, "lights");
        }

        private async Task SendMessage(int id, bool value, string subTopic)
        {
            var message = new SendMessageDto<bool>($"device/{id}/{subTopic}", value);
            await _mqqtClient.SendMessageAsync(message);
        }
    }
}
