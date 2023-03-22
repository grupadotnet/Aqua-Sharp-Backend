using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Profiles
{
    public class DeviceProfile: Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, CreateDeviceViewModel>().ReverseMap();
            CreateMap<Device, DeviceViewModel>().ReverseMap();
        }
    }
}
