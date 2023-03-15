using Models.ViewModels.Aquarium;

namespace Models.ViewModels.Device;

public class CreateDeviceViewModel
{
    public uint MeasurementFrequency { get; set; }
    public int AquariumId { get; set; }
}