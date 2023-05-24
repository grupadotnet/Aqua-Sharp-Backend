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

namespace Aqua_Sharp_Backend.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        //private readonly IMqttClient _client;
        //private readonly MqttClientOptions _clientOptions;

        public DeviceService(Context context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;

            var factory = new MqttFactory();
            //_client = factory.CreateMqttClient();
            //_clientOptions = new MqttClientOptionsBuilder()
            //    .WithTcpServer(configuration.GetValue<string>("Mqtt:Address")).Build();
            //_client.ConnectAsync(_clientOptions, CancellationToken.None);
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
            var deviceToUpdate = await _context.Devices.FindAsync(id);

            if (deviceToUpdate != null)
            {
                if (deviceToUpdate.Aquarium == null)
                {
                    deviceToUpdate.Aquarium = new Aquarium();
                }

                var editAquariumViewModel = new EditAquariumViewModel()
                {
                    Name = deviceToUpdate.Aquarium.Name,
                    Temperature = deviceToUpdate.Aquarium.Temperature,
                    PH = deviceToUpdate.Aquarium.PH,
                    Dawn = deviceToUpdate.Aquarium.Dawn,
                    Sunset = deviceToUpdate.Aquarium.Sunset
                };

                deviceModel.ApplyTo(editAquariumViewModel);

                if (string.IsNullOrEmpty(editAquariumViewModel.Name))
                {
                    editAquariumViewModel.Name = "Untitled"; // jeśli nie chcemy tej domyślnej nazwy nasza kolumna Name musi móc przyjmować wartość null
                }
                if (deviceModel.Operations.Any())
                {
                    deviceToUpdate.Aquarium.Name = editAquariumViewModel.Name;
                    deviceToUpdate.Aquarium.Temperature = editAquariumViewModel.Temperature;
                    deviceToUpdate.Aquarium.PH = editAquariumViewModel.PH;
                    deviceToUpdate.Aquarium.Dawn = editAquariumViewModel.Dawn;
                    deviceToUpdate.Aquarium.Sunset = editAquariumViewModel.Sunset;
                    deviceToUpdate.MeasurementFrequency = editAquariumViewModel.MeasurementFrequency;

                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> CheckIfDeviceExistsAsync(int id)
        {
            return await _context.Devices
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DeviceId == id) != null;
        }

        public async Task SwitchMode(int id, bool manual)
        {

            throw new NotImplementedException();
            //var device = await Get(id);
            //device.ManualMode = manual;
            //_context.Devices.Update(device);
            //await _context.SaveChangesAsync();

            //await SendMessage(id, manual, "mode");
        }
        public async Task SwitchLights(int id, bool lightsOn)
        {
            throw new NotImplementedException();
            //var device = await Get(id);

            //if (device.ManualMode == false)
            //{
            //    throw new BadRequest400Exception("Manual mode is turned off.");
            //}

            //await SendMessage(id, lightsOn, "lights");
        }

        //private async Task SendMessage(int id, bool value, string subTopic)
        //{
        //    if (!_client.IsConnected)
        //    {
        //        await _client.ConnectAsync(_clientOptions, CancellationToken.None);
        //    }

        //    var message = new MqttApplicationMessageBuilder()
        //        .WithTopic($"device/{id}/{subTopic}")
        //        .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
        //        .WithPayload(JsonSerializer.Serialize(value))
        //        .Build();
        //    await _client.PublishAsync(message);

        //}
    }
}
