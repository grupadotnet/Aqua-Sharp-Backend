using System.Text;
using System.Text.Json;
using Aqua_Sharp_Backend.Interfaces;
using Models.ViewModels.Measurement;
using MQTTnet.Client;
namespace Aqua_Sharp_Backend.Services;

public sealed class MeasurementMqttClientService: BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMqqtClientService _mqqtClientService;
    private Guid _objectId;
    
    public MeasurementMqttClientService(IMqqtClientService mqqtClientService, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mqqtClientService = mqqtClientService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _mqqtClientService.ConnectIfDisconnected();
        _objectId = await _mqqtClientService.SubscribeAsync("measurement/#", HandleMessageAsync);
    }
    
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _mqqtClientService.Unsubscribe(_objectId);
        await base.StopAsync(cancellationToken);
    }
    
    public override void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing) return;

        base.Dispose();
    }

    private async Task HandleMessageAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var payload=Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
        var measurementViewModel = JsonSerializer.Deserialize<CreateMeasurementViewModel>(payload);
        
        if(measurementViewModel is null) 
            return;

        using var scope = _serviceScopeFactory.CreateScope(); 
        var measurementService = scope.ServiceProvider.GetService<IMeasurementService>();
        
        if(measurementService is null)
            return;
        
        await measurementService.Create(measurementViewModel);
    }
}