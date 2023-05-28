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
            //.ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Sunset, opt => opt.MapFrom(src => src.Sunset))
            //.ForMember(dest => dest.Dawn, opt => opt.MapFrom(src => src.Dawn))
            //.ForMember(dest => dest.PH, opt => opt.MapFrom(src => src.PH))
            //.ForMember(dest => dest.Width, opt => opt.Ignore())
            //.ForMember(dest => dest.Length, opt => opt.Ignore())
            //.ForMember(dest => dest.Height, opt => opt.Ignore())
            //.ForMember(dest => dest.AquariumId, opt => opt.Ignore());


        }
    }
}
