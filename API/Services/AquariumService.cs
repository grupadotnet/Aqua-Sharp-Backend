using System.Globalization;
using System.Security.Claims;
using Aqua_Sharp_Backend.Authorization;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Exceptions;
using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Models.Entities;
using Models.ViewModels.Aquarium;
using Models.ViewModels.Device;

namespace Aqua_Sharp_Backend.Services
{
    public class AquariumService : IAquariumService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IDeviceService _deviceService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public AquariumService(Context context, IMapper mapper, IDeviceService deviceService,IAuthorizationService authorizationService,IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _deviceService = deviceService;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        
        public async Task<Aquarium> Add(CreateAquariumViewModel createAquariumViewModel)
        {
            
            var aquarium = _mapper.Map<Aquarium>(createAquariumViewModel);
            int? getUserId = _userContextService.GetUserId;
            aquarium.UserId =(getUserId is null) ? throw new Forbidden403Exception("403 Forbidden") :(int)getUserId;
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,new List<Aquarium>() {aquarium},new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
            {

                throw new Forbidden403Exception("403 Forbidden");

            }

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

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, new List<Aquarium>() { aquarium}, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded) {

                 throw new Forbidden403Exception("403 Forbidden");

            }

            _context.Remove(aquarium);
            await _context.SaveChangesAsync();
        }

        public Task<Aquarium> Edit(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<Aquarium>> GetAll()
        {
            List<Aquarium> aquariumList = null;
            if(_userContextService.GetUserRole == ((int)RoleName.Own).ToString())
            {
                aquariumList = await _context
                .Aquarium
                .AsNoTracking()
                .Where(a=>a.UserId==_userContextService.GetUserId)
                .ToListAsync();
            }
            else if(_userContextService.GetUserRole == ((int)RoleName.All).ToString())
            {
                aquariumList = await _context
                .Aquarium
                .AsNoTracking()
                .ToListAsync();
            }

            if (aquariumList == null)
                return new List<Aquarium>();

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, aquariumList, new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new Forbidden403Exception("403 Forbidden");
            }

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
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, new List<Aquarium> {aquarium}, new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new Forbidden403Exception("403 Forbidden");
            }
            
            return aquarium;
        }
        
        public async Task<Aquarium> GetForMeasurement(int id)
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
