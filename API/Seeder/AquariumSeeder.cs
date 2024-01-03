using Aqua_Sharp_Backend.Contexts;

namespace Aqua_Sharp_Backend.Seeder
{
    public class AquariumSeeder
    {
        private readonly Context _dbContext;
        
        public AquariumSeeder(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            if (!_dbContext.Database.CanConnect()) return;

            var pendingMigrations = _dbContext.Database.GetPendingMigrations();

            if(pendingMigrations !=  null && pendingMigrations.Any())
            {
                _dbContext.Database.Migrate();
            }
        }

        public void Seed()
        {
            if (!_dbContext.Database.CanConnect()) return;

            var pendingMigrations=_dbContext.Database.GetPendingMigrations();
            if(pendingMigrations != null && pendingMigrations.Any())
            {
                _dbContext.Database.Migrate();
            }

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
                        AquariumId = 1,
                        MeasurementFrequency = 5,
                        ManualMode = false
                    },
                    UserId = 1

                };
                    
                
                    
                
                    
                _dbContext.Aquarium.AddRange(aquarium1);
                _dbContext.SaveChanges();
            }

            var measurements = _dbContext.Measurements.ToList();
            if (!measurements.Any())
            {
                var createdMeasurements = new List<Measurement>
                {
                    new()
                    {
                        Time = new DateTime(2023, 3, 5, 15,0,0,DateTimeKind.Utc),
                        Temperature = 19,
                        TDS = 400 ,
                        LightOn = true,
                        AquariumId = 1
                    },
                    
                    new()
                    {
                        Time = new DateTime(2023, 3, 5, 15, 5, 0),
                        Temperature = 18,
                        TDS = 402,
                        LightOn = true,
                        AquariumId = 1
                    },
                    
                    new()
                    {
                        Time = new DateTime(2023, 3, 5, 15, 10, 0),
                        Temperature = 17,
                        TDS = 403,
                        LightOn = true,
                        AquariumId = 1
                    },
                    
                    new()
                    {
                        Time = new DateTime(2023, 3, 5, 15, 15, 0),
                        Temperature = 17,
                        TDS = 413,
                        LightOn = true,
                        AquariumId = 1
                    },
                    
                    new()
                    {
                        Time = new DateTime(2023, 3, 5, 15, 20, 0),
                        Temperature = 16,
                        TDS = 410,
                        LightOn = true,
                        AquariumId = 1
                    },
                    
                    new()
                    {
                        Time = new DateTime(2023, 3, 5, 15, 25, 0),
                        Temperature = 17,
                        TDS = 412,
                        LightOn = true,
                        AquariumId = 1
                    },
                    
                    new()
                    {
                        Time = new DateTime(2023, 3, 5, 15, 30, 0),
                        Temperature = 18,
                        TDS = 409,
                        LightOn = true,
                        AquariumId = 1
                    },
                    
                    new()
                    {
                        Time = new DateTime(2023, 3, 5, 15, 35, 0),
                        Temperature = 19,
                        TDS = 415,
                        LightOn = true,
                        AquariumId = 1
                    },
                    
                    new()
                    {
                        Time = new DateTime(2023, 3, 5, 15, 40, 0),
                        Temperature = 18,
                        TDS = 413,
                        LightOn = true,
                        AquariumId = 1
                    },
                    
                    
                };
                _dbContext.Measurements.AddRange(createdMeasurements);
                _dbContext.SaveChanges();
            }
        }
    }
}
