using Aqua_Sharp_Backend.Exceptions;
using UnitTests_Services.Base;

namespace UnitTests_Services.Tests;

public sealed class DeviceServiceTests : UnitTestsServicesBase
{
    
    [Fact]
    public async Task Add_Should_Return_True_If_Device_Is_Added_Successfully()
    {
        // Arrange
        SetupInMemoryDb();
        
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
            Device = null
        };
        
        var createDeviceViewModel = new CreateDeviceViewModel
        {
            MeasurementFrequency = 1,
            Aquarium = aquarium
        };
        
        await DbContext.Set<Aquarium>().AddAsync(aquarium);
        await DbContext.SaveChangesAsync();
        var deviceService = new DeviceService(DbContext, Mapper, Configuration);

        // Act
        var result = await deviceService.Add(createDeviceViewModel);

        // Assert
        Assert.True(result);
        Assert.Equal(1, await DbContext.Devices.CountAsync());
    }
    
    [Fact]
    public async Task Add_Should_Throw_BadRequest400Exception_If_Aquarium_Already_Has_Device()
    {
        // Arrange
        SetupInMemoryDb();
        
        var device = new Device
        {
            AquariumId = 1,
            MeasurementFrequency = 1,
            ManualMode = true
        };
        
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
            Device = device
        };
        
        var createDeviceViewModel = new CreateDeviceViewModel
        {
            MeasurementFrequency = 1,
            Aquarium = aquarium
        };
        
        await DbContext.Set<Aquarium>().AddAsync(aquarium);
        await DbContext.Set<Device>().AddAsync(device);
        await DbContext.SaveChangesAsync();
        var deviceService = new DeviceService(DbContext, Mapper, Configuration);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequest400Exception>(() => 
            deviceService.Add(createDeviceViewModel));
    }
}