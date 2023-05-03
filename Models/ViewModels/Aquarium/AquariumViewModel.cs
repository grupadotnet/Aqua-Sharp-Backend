using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels.Device;

namespace Models.ViewModels.Aquarium
{
    public class AquariumViewModel
    {
        public int AquariumId { get; set; }
        public string Name { get; set; }
        public uint Length { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public float Temperature { get; set; }
        public float PH { get; set; }
        public TimeOnly Dawn { get; set; }
        public TimeOnly Sunset { get; set; }

        public DeviceViewModel Device{ get; set; }
    }
}
