namespace Models.Entities
{
    public class Measurement 
    { 
        public int MeasurementId { get; set; }
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public uint TDS { get; set; }
        public bool LightOn { get; set; }

        public int AquariumId { get; set; }
        //public Aquarium Aquarium { get; set; }
    }
}
