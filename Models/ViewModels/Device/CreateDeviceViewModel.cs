namespace Models.ViewModels.Device;

public class CreateDeviceViewModel
{
    public uint MeasurementFrequency { get; set; }
    public bool ManualMode { get; set; } = false;
}