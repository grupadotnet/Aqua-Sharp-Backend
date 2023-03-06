using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Profiles
{
    public class DeviceProfile: Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, CreateDeviceViewModel>()
                .ReverseMap()
                .ForAllMembers(x => x.Ignore());
            CreateMap<Device, DeviceViewModel>()
                .ReverseMap()
                .ForAllMembers(x => x.Ignore());
        }
    }
}
