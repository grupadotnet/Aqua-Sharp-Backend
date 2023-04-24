using System.Diagnostics.Metrics;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Interfaces;
using Models.ViewModels.Measurement;

namespace Aqua_Sharp_Backend.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IAquariumService _aquariumService;

        public MeasurementService(Context context, IMapper mapper, IAquariumService aquariumService)
        {
            _context = context;
            _mapper = mapper;
            _aquariumService = aquariumService;
        }
        public async Task<Measurement> Create(CreateMeasurementViewModel viewModel)
        {
            await _aquariumService.Get(viewModel.AquariumId);
            
            var measurement = _mapper.Map<Measurement>(viewModel);
            var res = await _context.Measurements.AddAsync(measurement);
            await _context.SaveChangesAsync();

            return res.Entity;
        }

        public async Task Delete(int id)
        {
            var measurement = await _context.Measurements.AsNoTracking().FirstOrDefaultAsync(e => e.MeasurementId == id);

            if (measurement is null)
                return;

            _context.Measurements.Remove(measurement);

            await _context.SaveChangesAsync();
        }

        public Task<List<Measurement>> Get(int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<List<Measurement>> Get(int start, int end)
        {
            throw new NotImplementedException();
        }
    }
}
