using Models.ViewModels.Aquarium;

namespace Models.ViewModels.Device
{
    public class DeviceViewModel
    {
        public int Id { get; set; }
        public uint MeasurementFrequency { get; set; }
        public bool ManualMode { get; set; } = false;

        public AquariumViewModel Aquarium { get; set; }
        public int AquariumId { get; set; }
    }
}
