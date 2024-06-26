using System.Diagnostics.Metrics;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Exceptions;
using Aqua_Sharp_Backend.Interfaces;
using Aqua_Sharp_Backend.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.ViewModels.Measurement;

namespace Aqua_Sharp_Backend.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IAquariumService _aquariumService;
        private const Int32 PageSize = 20;
        private readonly IHubContext<AquaSharpHub> _hubContext;

        public MeasurementService(Context context, IMapper mapper, IAquariumService aquariumService, IHubContext<AquaSharpHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _aquariumService = aquariumService;
            _hubContext = hubContext;
        }

        public async Task<Measurement> Create(CreateMeasurementViewModel viewModel)
        {
            await _aquariumService.GetForMeasurement(viewModel.AquariumId);
            var measurement = _mapper.Map<Measurement>(viewModel);
            var userId = _context.Aquarium.FirstOrDefault(a => a.AquariumId == measurement.AquariumId).UserId;
            var res = await _context.Measurements.AddAsync(measurement);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.User(userId.ToString()).SendAsync("SendMeasure", measurement);
            return res.Entity;
        }

        public async Task Delete(int id)
        {
            var measurement =
                await _context.Measurements.AsNoTracking().FirstOrDefaultAsync(e => e.MeasurementId == id);

            if (measurement is null)
                return;

            _context.Measurements.Remove(measurement);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Measurement>> Get(GetMeasurementsPaginationViewModel paginationViewModel)
        {
            if (paginationViewModel.Page < 1)
            {
                throw new BadRequest400Exception("Page must be a number greater than 0");
            }
            
            await this._aquariumService.Get(paginationViewModel.AquariumId);
            
            var skip = (paginationViewModel.Page - 1) * PageSize;

            var res = await _context.Measurements
                .Where(e => e.AquariumId == paginationViewModel.AquariumId)
                .OrderByDescending(e => e.MeasurementId)
                .Skip(skip)
                .Take(PageSize)
                .ToListAsync();

            return res;
        }

        public async Task<List<Measurement>> Get(GetMeasurementsStartFromViewModel viewModel)
        {
            if (viewModel.StartFrom < 1)
            {
                throw new BadRequest400Exception("StartFrom must be a number greater than 0");

            }
            
            await this._aquariumService.Get(viewModel.AquariumId);
            
            var res = await _context.Measurements
                .Where(e => e.AquariumId == viewModel.AquariumId && e.MeasurementId >= viewModel.StartFrom)
                .Take(PageSize)
                .ToListAsync();

            return res;
        }

        
        
    }
}