using Aqua_Sharp_Backend.Contexts;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Aqua_Sharp_Backend
{
    public class AquariumSeeder
    {

        private readonly Context _dbContext;
        public AquariumSeeder(Context dbContext)
        {
            _dbContext = dbContext;

        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var aquarius = _dbContext.Aquarium.ToList();
                if (!aquarius.Any())
                {
                    var aquarium1 = new Aquarium()
                    {
                        Name = "Aquarium1",
                        Length = 45,
                        Width = 32,
                        Height = 35,
                        Temperature = 18,
                        PH = 7,
                        Dawn = new TimeOnly(6, 15),
                        Sunset = new TimeOnly(20, 30),
                        Device = new Device()
                        {
                            MeasurementFrequency = 5,
                            ManualMode = false,
                        },

                    };


                    var aquarium2 = new Aquarium()
                    {
                        Name = "Aquarium2",
                        Length = 87,
                        Width = 56,
                        Height = 34,
                        Temperature = 19,
                        PH = 6,
                        Dawn = new TimeOnly(6, 20),
                        Sunset = new TimeOnly(21, 30),
                        Device = new Device()
                        {
                            MeasurementFrequency = 2,
                            ManualMode = false,
                        },










                    };


                    _dbContext.Aquarium.AddRange(aquarium1, aquarium2);
                    _dbContext.SaveChanges();

                }

                var measurements = _dbContext.Measurements.ToList();
                if (!measurements.Any())
                {
                    var measurement1 = new Measurement()
                    {


                        Time = new DateTime(),
                        Temperature = 19,
                        TDS = 7,
                        LightOn = true,
                        AquariumId = 1,


                    };

                    var measurement2 = new Measurement()
                    {
                        Time = new DateTime(),
                        Temperature = 19,
                        TDS = 7,
                        LightOn = false,
                        AquariumId = 1
                    };

                    var measurement3 = new Measurement()
                    {
                        Time = new DateTime(),
                        Temperature = 17,
                        TDS = 8,
                        LightOn = false,
                        AquariumId = 2
                    };
                    var measurement4 = new Measurement()
                    {
                        Time = new DateTime(),
                        Temperature = 17,
                        TDS = 8,
                        LightOn = false,
                        AquariumId = 3
                    };



                    _dbContext.Measurements.AddRange(measurement1, measurement2, measurement3, measurement4);
                    _dbContext.SaveChanges();
                }




            }
        }



    }
}
