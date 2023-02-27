using System.Globalization;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Exceptions;
using Aqua_Sharp_Backend.Interfaces;
using Models.Entities;
using Models.ViewModels.Aquarium;

namespace Aqua_Sharp_Backend.Services
{
    public class AquariumService : IAquariumService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public AquariumService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public Task<Aquarium> Add(CreateAquariumViewModel createAquariumViewModel)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Aquarium> Edit(int Id)
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

        public async Task<Aquarium> GetOne(int id)
        {
            var aquarium = await _context
                .Aquarium
                .AsNoTracking()
                .Include(a => a.Device)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aquarium == null)
                throw new NotFound404Exception($"404. Aquarium with id: {id} not found!");
            
            return aquarium;
        }
    }
}
