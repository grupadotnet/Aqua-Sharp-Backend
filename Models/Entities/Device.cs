using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public uint MeasurementFrequency { get; set; }
        public bool ManualMode { get; set; } = false;

        public Aquarium Aquarium { get; set; }
        

    }
}
