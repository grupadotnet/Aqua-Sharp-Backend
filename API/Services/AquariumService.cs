using Aqua_Sharp_Backend.Contexts;
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

        public Task<List<Aquarium>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<AquariumViewModel?> GetOne(int id)
        {
            var aquarium = await _context.Aquarium
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            if (aquarium == null)
                return null;
            
            var aquariumViewModel = _mapper.Map<AquariumViewModel>(aquarium);
            return aquariumViewModel;
        }
    }
}
