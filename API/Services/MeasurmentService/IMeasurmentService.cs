using Models.Models;

namespace Aqua_Sharp_Backend.Services.MeasurmentService
{
    public interface IMeasurmentService
    {
        Task<Measurement> Add();
        Task<List<Measurement>> Get(int pageNumber);
        Task<List<Measurement>> Get(int start, int end);
        Task Delete(int id);
    }   
}
