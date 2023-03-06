using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Interfaces
{
    public interface IDeviceService
    {
        Task<Device> Add(CreateDeviceViewModel createDeviceViewModel);
        Task<Device> Update();
        Task<Device> Get();
        Task Delete();
        Task GetDeviceConfig(int Id);
    }
}
