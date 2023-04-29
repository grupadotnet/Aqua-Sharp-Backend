using System.Globalization;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Exceptions;
using Aqua_Sharp_Backend.Interfaces;
using Models.ViewModels.Aquarium;
using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Services
{
    public class AquariumService : IAquariumService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IDeviceService _deviceService;

        public AquariumService(Context context, IMapper mapper, IDeviceService deviceService)
        {
            _context = context;
            _mapper = mapper;
            _deviceService = deviceService;
        }
        
        public async Task<Aquarium> Add(CreateAquariumViewModel createAquariumViewModel)
        {
            var aquarium = _mapper.Map<Aquarium>(createAquariumViewModel);
            var addedAquarium = await _context.Aquarium.AddAsync(aquarium);

            var createDeviceViewModel = new CreateDeviceViewModel()
            {
                Aquarium = addedAquarium.Entity,
                MeasurementFrequency = createAquariumViewModel.MeasurementFrequency
            };
           await _deviceService.Add(createDeviceViewModel);

           await _context.SaveChangesAsync();

           var addedAquariumWithDevice = await Get(addedAquarium.Entity.AquariumId);
           
           return addedAquariumWithDevice;
        }

        public async Task Delete(int id)
        {
            var aquarium = await _context
                .Aquarium
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AquariumId == id);
            
            _context.Remove(aquarium);
            await _context.SaveChangesAsync();
        }

        public Task<Aquarium> Edit(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<Aquarium>> GetAll()
        {
            var aquariumList = await _context
                .Aquarium
                .AsNoTracking()
                .ToListAsync();

            if (aquariumList == null)
                return new List<Aquarium>();

            return aquariumList;
        }

        public async Task<Aquarium> Get(int id)
        {
            var aquarium = await _context
                .Aquarium
                .AsNoTracking()
                .Include(a => a.Device)
                .FirstOrDefaultAsync(a => a.AquariumId == id);

            if (aquarium == null) throw new NotFound404Exception(
                $"404. Aquarium with id: {id} not found!");
            
            return aquarium;
        }
        
        public async Task<bool> CheckIfAquariumExistsAsync(int id)
        {
            return await _context.Aquarium
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AquariumId == id) != null;
        }
    }
}
