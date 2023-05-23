using System.Text;
using System.Text.Json;
using Aqua_Sharp_Backend.Interfaces;
using Models.ViewModels.Measurement;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Security.Authentication;
namespace Aqua_Sharp_Backend.Services;

public sealed class MqttClientService: BackgroundService
{
    private readonly IMqttClient _client;
    private readonly MqttClientOptions _clientOptions;
    private readonly MqttClientSubscribeOptions _subscriptionOptions;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MqttClientService(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        
        var factory = new MqttFactory();
        _client = factory.CreateMqttClient();
        _clientOptions = new MqttClientOptionsBuilder()
            .WithClientId("Backend")
            .WithTcpServer(configuration.GetValue<string>("Mqtt:Address"))
            .WithCredentials("mqtt-aquasharp-westeu-dev-001.azure-devices.net/Backend", "ipANrXkE+GV2Ts7nNh9Fh9X7g8ZNUqWXRul+jb1am64=")
            .WithTls(new MqttClientOptionsBuilderTlsParameters
            {
                UseTls = true,
             })
            .Build();
        _subscriptionOptions = factory.CreateSubscribeOptionsBuilder().WithTopicFilter(f =>
        {
            f.WithTopic("measurement/#");
            f.WithAtMostOnceQoS(); ;
        }).Build();
        
        _client.ApplicationMessageReceivedAsync += HandleMessageAsync;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _client.ConnectAsync(_clientOptions, CancellationToken.None);
        await _client.SubscribeAsync(_subscriptionOptions, CancellationToken.None);
    }
    
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.DisconnectAsync();
        await base.StopAsync(cancellationToken);
    }
    
    public override void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing) return;
        
        _client.Dispose();
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
        
        var res = await measurementService.Create(measurementViewModel);
        await PublishVerifiedMeasurementAsync(res);
    }

    private async Task PublishVerifiedMeasurementAsync(Measurement measurement)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic("verified/measurement/" + measurement.AquariumId)
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
            .WithPayload(JsonSerializer.Serialize(measurement))
            .Build();
        
        await _client.PublishAsync(message);
    }

}