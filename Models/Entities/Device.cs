using System.Text.Json.Serialization;

namespace Models.Entities
{
    public class Device
    {
        public int DeviceId { get; set; }
        public uint MeasurementFrequency { get; set; }
        public bool ManualMode { get; set; } = false;
        
        public int AquariumId { get; set; }

        [JsonIgnore]
        public Aquarium Aquarium { get; set; }
    }
}
