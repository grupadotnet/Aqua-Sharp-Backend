using Models.ViewModels.Aquarium;

namespace Aqua_Sharp_Backend.Services.AquariumService
{
    public interface IAquariumService
    {
        Task<Aquarium> Add(CreateAquariumViewModel createAquariumViewModel);
        Task Delete(int id);
        Task<Aquarium> GetOne(int id);
        Task<List<Aquarium>> GetAll();
        Task<Aquarium> Edit(int id);
    }
}
