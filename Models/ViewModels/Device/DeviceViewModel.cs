using Models.ViewModels.Aquarium;

namespace Models.ViewModels.Device
{
    public class DeviceViewModel
    {
        public int DeviceId { get; set; }
        public uint MeasurementFrequency { get; set; }
        public bool ManualMode { get; set; } = false;
        
        public int AquariumId { get; set; }
        public AquariumViewModel Aquarium { get; set; }
    }
}
