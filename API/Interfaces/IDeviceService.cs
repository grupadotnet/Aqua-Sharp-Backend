using Models.Entities;

namespace Aqua_Sharp_Backend.Interfaces
{
    public interface IDeviceService
    {
        Task<Device> Create();
        Task<Device> Update();
        Task<Device> Get();
        Task GetDeviceConfig();
        Task Delete();
    }
}
