using Aqua_Sharp_Backend.Contexts;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Aqua_Sharp_Backend.Seeder
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
                        Id = 1,
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
                        Id = 2,
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

                    List<Measurement> createdMeasurements = new List<Measurement>(){


                    new Measurement()
                    {


                        Time = new DateTime(2023, 3, 5, 15,0,0,DateTimeKind.Utc),
                        Temperature = 19,
                        TDS = 400 ,
                        LightOn = true,
                        AquariumId = 1



                     },
                    new Measurement()
                    {


                        Time = new DateTime(2023, 3, 5, 15, 5, 0),
                        Temperature = 18,
                        TDS = 402,
                        LightOn = true,
                        AquariumId = 1



                    },
                    new Measurement()
                    {


                        Time = new DateTime(2023, 3, 5, 15, 10, 0),
                        Temperature = 17,
                        TDS = 403,
                        LightOn = true,
                        AquariumId = 1



                    },
                    new Measurement()
                    {


                        Time = new DateTime(2023, 3, 5, 15, 15, 0),
                        Temperature = 17,
                        TDS = 413,
                        LightOn = true,
                        AquariumId = 1



                    },
                    new Measurement()
                    {


                        Time = new DateTime(2023, 3, 5, 15, 20, 0),
                        Temperature = 16,
                        TDS = 410,
                        LightOn = true,
                        AquariumId = 1



                    },
                    new Measurement()
                    {


                        Time = new DateTime(2023, 3, 5, 15, 25, 0),
                        Temperature = 17,
                        TDS = 412,
                        LightOn = true,
                        AquariumId = 1



                    },
                    new Measurement()
                    {


                        Time = new DateTime(2023, 3, 5, 15, 30, 0),
                        Temperature = 18,
                        TDS = 409,
                        LightOn = true,
                        AquariumId = 1



                    },
                    new Measurement()
                    {


                        Time = new DateTime(2023, 3, 5, 15, 35, 0),
                        Temperature = 19,
                        TDS = 415,
                        LightOn = true,
                        AquariumId = 1



                    },
                    new Measurement()
                    {


                        Time = new DateTime(2023, 3, 5, 15, 40, 0),
                        Temperature = 18,
                        TDS = 413,
                        LightOn = true,
                        AquariumId = 1



                    },


                    new Measurement()
                    {

                        Time = new DateTime(2023, 3, 5, 15, 0, 0),
                        Temperature = 19,
                        TDS = 442,
                        LightOn = false,
                        AquariumId = 2
                    },

                    new Measurement()
                    {

                        Time = new DateTime(2023, 3, 5, 15, 5, 0),
                        Temperature = 17,
                        TDS = 440,
                        LightOn = false,
                        AquariumId = 2
                    },
                    new Measurement()
                    {

                        Time = new DateTime(2023, 3, 5, 15, 10, 0),
                        Temperature = 17,
                        TDS = 450,
                        LightOn = false,
                        AquariumId = 2
                    },
                    new Measurement()
                    {

                        Time = new DateTime(2023, 3, 5, 15, 15, 0),
                        Temperature = 18,
                        TDS = 445,
                        LightOn = false,
                        AquariumId = 2
                    },
                    new Measurement()
                    {

                        Time = new DateTime(2023, 3, 5, 15, 20, 0),
                        Temperature = 19,
                        TDS = 440,
                        LightOn = false,
                        AquariumId = 2
                    },
                    new Measurement()
                    {

                        Time = new DateTime(2023, 3, 5, 15, 25, 0),
                        Temperature = 17,
                        TDS = 441,
                        LightOn = false,
                        AquariumId = 2
                    },
                    new Measurement()
                    {

                        Time = new DateTime(2023, 3, 5, 15, 30, 0),
                        Temperature = 18,
                        TDS = 445,
                        LightOn = false,
                        AquariumId = 2
                    },
                    new Measurement()
                    {

                        Time = new DateTime(2023, 3, 5, 15, 35, 0),
                        Temperature = 18,
                        TDS = 435,
                        LightOn = false,
                        AquariumId = 2
                    },
                    new Measurement()
                    {

                        Time = new DateTime(2023, 3, 5, 15, 40, 0),
                        Temperature = 19,
                        TDS = 430,
                        LightOn = false,
                        AquariumId = 2
                    }





                    };


                    _dbContext.Measurements.AddRange(createdMeasurements);

                    _dbContext.SaveChanges();
                }




            }
        }


    }
}
