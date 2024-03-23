using Models.Entities;
using Models.ViewModels.Aquarium;
using System.Security.Claims;

namespace Aqua_Sharp_Backend.Interfaces
{
    public interface IAquariumService
    {
        Task<Aquarium> Add(CreateAquariumViewModel createAquariumViewModel);
        Task Delete(int id);
        Task<Aquarium> Get(int id);
        Task<List<Aquarium>> GetAll();
        Task<Aquarium> Edit(int id);
        Task<bool> CheckIfAquariumExistsAsync(int id);
    }
}
