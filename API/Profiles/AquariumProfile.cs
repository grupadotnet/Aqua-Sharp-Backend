using Models.ViewModels.Aquarium;

namespace Aqua_Sharp_Backend.Profiles
{
    public class AquariumProfile: Profile
    {
        public AquariumProfile()
        {
            CreateMap<Aquarium, CreateAquariumViewModel>().ReverseMap().ForAllMembers(x => x.Ignore());
            CreateMap<Aquarium, AquariumViewModel>().ReverseMap().ForAllMembers(x => x.Ignore());
        }
    }
}
