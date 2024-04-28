using Models.Entities;
using Models.ViewModels.Measurement;

namespace Aqua_Sharp_Backend.Interfaces
{
    public interface IMeasurementService
    {
        Task<Measurement> Create(CreateMeasurementViewModel viewModel);
        Task<List<Measurement>> Get(GetMeasurementsPaginationViewModel paginationViewModel);
        Task<List<Measurement>> Get(GetMeasurementsStartFromViewModel viewModel);
        Task Delete(int id);
        Task SendMes(int userID);
    }
}
