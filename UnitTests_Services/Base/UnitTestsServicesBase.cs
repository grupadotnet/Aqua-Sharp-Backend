using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace UnitTests_Services.Base;

public class UnitTestsServicesBase
{
    protected Context DbContext { get; private set; }
    protected IMapper Mapper { get; private set; }
    protected IConfiguration Configuration { get; private set; }

    protected UnitTestsServicesBase()
    {
        SetupMapper();
        SetupConfiguration();
    }
    
    protected void SetupInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("test_database")
            .Options;

        DbContext = new Context(options);
    }

    private void SetupMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile(new ConfigProfile()); 
            cfg.AddProfile(new AquariumProfile()); 
            cfg.AddProfile(new DeviceProfile()); 
            cfg.AddProfile(new MeasurementProfile()); 
        });

        Mapper = new Mapper(mapperConfig);
    }

    private void SetupConfiguration()
    {
        var configuration = new Dictionary<string, string>
        {
            {"Mqtt:Address", "localhost"}
        };
            
        Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configuration)
            .Build();
    }
}