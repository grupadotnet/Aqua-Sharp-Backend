namespace Models.ViewModels.Measurement;

public class CreateMeasurementViewModel
{
    public DateTime Time { get; set; }
    public float Temperature { get; set; }
    public float Ph { get; set; }
    public uint TDS { get; set; }
    public bool LightOn { get; set; }
    public int AquariumId { get; set; }
}
