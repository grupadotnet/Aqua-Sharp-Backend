using AutoMapper;

namespace UnitTests_Services.Base
{
    public abstract class ServiceTestBase
    {
        protected static readonly Mock<Context> MockContext = new();
        protected static readonly Mock<DbSet<Aquarium>> MockSetAquarium = new();
        protected static readonly Mock<DbSet<Config>> MockSetConfig = new();
        protected static readonly Mock<DbSet<Device>> MockSetDevice = new();
        protected static readonly Mock<DbSet<Measurement>> MockSetMeasurement = new();
        
        protected static IMapper Mapper { get; private set; }
        
        protected ServiceTestBase()
        {
            SetupMockContext();
            CreateMapper();
        }

        private void SetupMockContext()
        {
            MockContext.Setup(m => m.Aquarium).Returns(MockSetAquarium.Object);
            MockContext.Setup(m => m.Config).Returns(MockSetConfig.Object);
            MockContext.Setup(m => m.Devices).Returns(MockSetDevice.Object);
            MockContext.Setup(m => m.Measurements).Returns(MockSetMeasurement.Object);
        }

        private void CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new ConfigProfile()); 
                cfg.AddProfile(new AquariumProfile()); 
                cfg.AddProfile(new DeviceProfile()); 
                cfg.AddProfile(new MeasurmentProfile()); 
            });

            Mapper = new Mapper(mapperConfig);
        }
    }
}