using System.Text.Json;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Exceptions;
using Aqua_Sharp_Backend.Interfaces;
using Models.ViewModels.Device;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace Aqua_Sharp_Backend.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IMqttClient _client;
        private readonly MqttClientOptions _clientOptions;

        public DeviceService(Context context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;

            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();
            _clientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(configuration.GetValue<string>("Mqtt:Address")).Build();
            _client.ConnectAsync(_clientOptions, CancellationToken.None);
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
            if (!_client.IsConnected)
            {
                await _client.ConnectAsync(_clientOptions, CancellationToken.None);
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic($"device/{id}/{subTopic}")
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                .WithPayload(JsonSerializer.Serialize(value))
                .Build();
            await _client.PublishAsync(message);
            
        }
}
}
