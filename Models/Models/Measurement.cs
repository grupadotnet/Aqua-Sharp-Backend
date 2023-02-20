namespace Models.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public int IdAquarium { get; set; }
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public uint TDS { get; set; }
        public bool LightOn { get; set; }
    }
}
