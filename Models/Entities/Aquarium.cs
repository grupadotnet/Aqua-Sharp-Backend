﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Aquarium
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
        public int UserId { get; set; }
        public Device Device { get; set; }
        public User User { get; set; }

        

    }
}
