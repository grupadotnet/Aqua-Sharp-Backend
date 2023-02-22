using Models.Entities;
using Models.ViewModels.Aquarium;

namespace Aqua_Sharp_Backend.Interfaces
{
    public interface IAquariumService
    {
        Task<Aquarium> Add(CreateAquariumViewModel createAquariumViewModel);
        Task Delete(int id);
        Task<AquariumViewModel?> GetOne(int id);
        Task<List<Aquarium>> GetAll();
        Task<Aquarium> Edit(int id);
    }
}
