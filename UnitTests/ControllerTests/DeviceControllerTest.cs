using Models.ViewModels.Device;
using Xunit.Abstractions;

namespace UnitTests.ControllerTests;

public sealed class DeviceControllerTest : ControllerTestBase
{
    #region Properties

    private DeviceController _deviceController;
    private readonly Mock<IDeviceService> _deviceServiceMock = new();

    #endregion

    public DeviceControllerTest(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async void GetTest()
    {
        // Arrange
        var device = new Device()
        {
            DeviceId = 1,
            MeasurementFrequency = 1,
            ManualMode = true,
            AquariumId = 1,
            Aquarium = new Aquarium()
            {
                Name = "Aquarium1",
                Length = 1,
                Width = 1,
                Height = 1,
                Temperature = 1,
                PH = 1,
                Dawn = new TimeOnly(1, 1),
                Sunset = new TimeOnly(1, 1),
                Device = new Device()
            }
        };

        _deviceServiceMock.Setup(a => a.Get(1)).ReturnsAsync(device);
        _deviceController = new DeviceController(_deviceServiceMock.Object, Mapper);

        // Act
        var result = await _deviceController.Get(1);
        var objResult = GetValueFromResult<DeviceViewModel>(result);
        
        // Assert
        Assert.NotNull(objResult.Object);
        Assert.NotNull(objResult.Object.Aquarium);
        Assert.Equal(Success200, objResult.Status);
        Assert.Equal((uint)1, objResult.Object.MeasurementFrequency);
        Assert.Equal("Aquarium1", objResult.Object.Aquarium.Name);
        
        Output.WriteLine($"Status code: {objResult.Status}");
    }
}