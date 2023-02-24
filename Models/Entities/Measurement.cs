namespace Models.Entities
{
    public class Measurement
    {
        public int Id { get; set; }
        
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public uint TDS { get; set; }
        public bool LightOn { get; set; }

        //public Aquarium Aquarium { get; set; }
        public int AquariumId { get; set; }
    }
}
