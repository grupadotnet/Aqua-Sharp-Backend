using UnitTests_Services.Base;

namespace UnitTests_Services.Tests
{
    [TestClass]
    public sealed class DeviceServiceTest : ServiceTestBase
    {
        #region Properties
        
        private static readonly IAquariumService AquariumService = 
            new AquariumService(MockContext.Object, Mapper);
        
        private static readonly IDeviceService DeviceService = 
            new DeviceService(MockContext.Object, Mapper, AquariumService);

        #endregion

        [TestMethod]
        public async Task AddTest()
        {
            var createDeviceViewModel = new CreateDeviceViewModel()
            {
                MeasurementFrequency = 1,
                AquariumId = 1
            };
            
            await DeviceService.Add(createDeviceViewModel);
            
                // var mockSetDevice = new Mock<DbSet<Device>>();
                // MockContext.Setup(m => m.Devices).Returns(mockSetDevice.Object);
            
            MockSetDevice.Verify(m => m.Add(It.IsAny<Device>()), Times.Once());
            MockContext.Verify(m => m.SaveChanges(), Times.Once());
            
            // --------------
            
            // var mockSet = new Mock<DbSet<Device>>();
            //
            // var mockContext = new Mock<Context>();
            // mockContext.Setup(m => m.Devices).Returns(mockSet.Object);
            //
            // var service = new DeviceService(mockContext.Object, Mapper, AquariumService);
            // await service.Add(createDeviceViewModel);
            //
            // mockSet.Verify(m => m.Add(It.IsAny<Device>()), Times.Once());
            // mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}

