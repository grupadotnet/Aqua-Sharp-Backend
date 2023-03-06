using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Interfaces;
using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        
        public DeviceService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Device> Add(CreateDeviceViewModel createDeviceViewModel)
        {
            var deviceToAdd = _mapper.Map<Device>(createDeviceViewModel);
            
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
