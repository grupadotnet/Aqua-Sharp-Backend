namespace Models.Entities
{
    public class Measurement
    {

        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public uint TDS { get; set; }
        public bool LightOn { get; set; }
        public int AquariumId { get; set; }
    }
}
