using Models.Entities;
using Models.ViewModels.Measurement;

namespace Aqua_Sharp_Backend.Interfaces
{
    public interface IMeasurementService
    {
        Task<Measurement> Create(CreateMeasurementViewModel viewModel);
        Task<List<Measurement>> Get(int pageNumber);
        Task<List<Measurement>> Get(int start, int end);
        Task Delete(int id);
    }
}
