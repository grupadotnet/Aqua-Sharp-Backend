using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.Aquarium
{
    public class EditAquariumViewModel
    {
        public string? Name { get; set; }
        public float Temperature { get; set; }
        public float PH { get; set; }
        public TimeOnly Dawn { get; set; }
        public TimeOnly Sunset { get; set; }
        public uint MeasurementFrequency { get; set; }
    }
}
