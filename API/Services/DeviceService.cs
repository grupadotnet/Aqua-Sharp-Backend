using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Interfaces;
using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IAquariumService _aquariumService;
        
        public DeviceService(Context context, IMapper mapper, IAquariumService aquariumService)
        {
            _context = context;
            _mapper = mapper;
            _aquariumService = aquariumService;
        }
        
        public async Task<Device> Add(CreateDeviceViewModel createDeviceViewModel)
        {
            await _aquariumService.GetOne(createDeviceViewModel.AquariumId);
            
            var deviceToAdd = new Device
            {
                MeasurementFrequency = createDeviceViewModel.MeasurementFrequency,
                AquariumId = createDeviceViewModel.AquariumId
            };

            var addedDevice = await _context.Devices.AddAsync(deviceToAdd);

            await _context.SaveChangesAsync();

            return addedDevice.Entity;
        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public Task<Device> Get()
        {
            throw new NotImplementedException();
        }

        public Task GetDeviceConfig(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Device> Update()
        {
            throw new NotImplementedException();
        }
    }
}
