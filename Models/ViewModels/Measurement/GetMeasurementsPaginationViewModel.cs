using System.Runtime.InteropServices;

namespace Models.ViewModels.Measurement;

public class GetMeasurementsPaginationViewModel
{
    public Int32 AquariumId { get; set; }
    public Int32 Page { get; set; }
}