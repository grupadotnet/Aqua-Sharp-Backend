using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Exceptions;
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
            var aquariumId = createDeviceViewModel.AquariumId;
            
            var aquarium = await _aquariumService.Get(aquariumId);
            
            if (aquarium.Device != null) throw new BadRequest400Exception(
                $"400. Aquarium with id: {aquariumId} already has a device!");

            var deviceToAdd = _mapper.Map<Device>(createDeviceViewModel);
            
            var addedDevice = await _context.Devices.AddAsync(deviceToAdd);
            await _context.SaveChangesAsync();

            return addedDevice.Entity;
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
            
            if (device == null) throw new NotFound404Exception(
                    $"404. Aquarium with id: {id} not found!");
            
            return device;
        }

        public async Task <DeviceConfigViewModel> GetDeviceConfig (int id)
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
    }
}
