
namespace Aqua_Sharp_Backend.Services.DeviceService
{
    public interface IDeviceService
    {
        Task<Device> Create();
        Task<Device> Update();
        Task<Device> Get();
        Task Delete();
    }
}
