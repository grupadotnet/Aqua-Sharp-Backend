using Models.ViewModels.Measurement;

namespace Aqua_Sharp_Backend.Profiles
{
    public class MeasurementProfile : Profile
    {
        public MeasurementProfile()
        {
            CreateMap<CreateMeasurementViewModel, Measurement>()
                .ReverseMap();
        }
    }
}