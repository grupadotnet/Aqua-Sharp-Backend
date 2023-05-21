using Microsoft.AspNetCore.JsonPatch;
using Models.ViewModels.Aquarium;
using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Interfaces
{
    public interface IDeviceService
    {
        Task<Device> Add(CreateDeviceViewModel createDeviceViewModel);
        Task Update(int id, JsonPatchDocument<EditAquariumViewModel> deviceModel);
        Task<Device> Get(int id);
        Task Delete();
        Task<DeviceConfigViewModel> GetDeviceConfig(int id);
        Task<bool> CheckIfDeviceExistsAsync(int id);
        Task SwitchMode(int id, bool manual);
        Task SwitchLights(int id, bool lightsOn);
    }
}
