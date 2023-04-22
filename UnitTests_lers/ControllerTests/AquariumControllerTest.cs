
using AutoMapper;
using Models.ViewModels.Aquarium;
using Xunit.Abstractions;

namespace UnitTests.ControllerTests;

public sealed class AquariumControllerTest : ControllerTestBase
{
    #region Properties

    private AquariumController _aquariumController;
    private readonly Mock<IAquariumService> _aquariumServiceMock = new();

    #endregion

    public AquariumControllerTest(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async void GetTest()
    {
        // Arrange
        var aquarium = new Aquarium()
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
            {
                MeasurementFrequency = 1,
                ManualMode = false
            }
        };

        _aquariumServiceMock.Setup(a => a.Get(1)).ReturnsAsync(aquarium);
        _aquariumController = new AquariumController(_aquariumServiceMock.Object, Mapper);

        // Act
        var result = await _aquariumController.Get(1);
        var objResult = GetValueFromResult<AquariumViewModel>(result);
        
        // Assert
        Assert.NotNull(objResult.Object);
        Assert.NotNull(objResult.Object.Device);
        Assert.Equal(Success200, objResult.Status);
        Assert.Equal("Aquarium1", objResult.Object.Name);
        Assert.Equal((uint)1, objResult.Object.Device.MeasurementFrequency);
        
        Output.WriteLine($"Status code: {objResult.Status}");
    }
}