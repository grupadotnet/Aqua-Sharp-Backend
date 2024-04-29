namespace Aqua_Sharp_Backend.SignalR
{
    public class SendMes
    {
        public int MeasurementId { get; set; }
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public float Ph { get; set; }
        public uint TDS { get; set; }
        public bool LightOn { get; set; }
    }
}
