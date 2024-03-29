﻿namespace Models.Entities
{
    public class Device
    {
        public int DeviceId { get; set; }
        public uint MeasurementFrequency { get; set; }
        public bool ManualMode { get; set; } = false;
        public string? AzureDeviceName { get; set; }
        public int AquariumId { get; set; }
        public Aquarium Aquarium { get; set; }
    }
}
