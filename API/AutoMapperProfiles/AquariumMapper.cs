using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Models.ViewModels.Aquarium;

namespace Aqua_Sharp_Backend.MapperProfiles
{
    public class AquariumMapper : Profile
    {
        public AquariumMapper()
        {
            CreateMap<EditAquariumViewModel, Aquarium>().ReverseMap();

        }
    }
}
