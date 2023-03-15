using System.Text;
using MQTTnet;
using MQTTnet.Client;

namespace Aqua_Sharp_Backend.Services;

public class MqttClientService: BackgroundService
{
    private readonly IMqttClient _client;
    private readonly MqttClientOptions _clientOptions;
    private readonly MqttClientSubscribeOptions _subscriptionOptions;

    public MqttClientService(IConfiguration configuration)
    {
        var factory = new MqttFactory();
        _client = factory.CreateMqttClient();

        _clientOptions = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();
        _subscriptionOptions = factory.CreateSubscribeOptionsBuilder().WithTopicFilter(f =>
        {
            f.WithTopic("measurement/#");
            f.WithAtMostOnceQoS();
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

    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
        {
            _client.Dispose();
            base.Dispose();
        }
    }

    private async Task HandleMessageAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var payload=Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
    
    }

}