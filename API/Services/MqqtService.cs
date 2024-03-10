using Aqua_Sharp_Backend.Interfaces;
using Models.Delegates.MQTT;
using Models.DTO.MQTT;
using MQTTnet;
using MQTTnet.Client;
using System.Text.Json;
using System.Text.RegularExpressions;
using Handlers =
    System.Collections.Generic.List<(string topic, System.Text.RegularExpressions.Regex topicRegex,
        Models.Delegates.MQTT.HandleMessageAsyncDelegate handler, System.Guid objectId)>;

namespace Aqua_Sharp_Backend.Services;

public class MqqtService : IMqqtClientService
{
    private readonly MqttClientOptions _clientOptions;
    private readonly IMqttClient _client;
    private Handlers _handlers;

    public MqqtService(IConfiguration configuration)
    {
        _handlers = new Handlers();
        var factory = new MqttFactory();
        _client = factory.CreateMqttClient();
        _clientOptions = new MqttClientOptionsBuilder()
            .WithClientId("Backend")
            .WithTcpServer(configuration.GetValue<string>("Mqtt:Address"))
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(5))
            .Build();
        _client.ApplicationMessageReceivedAsync += HandleMessageAsync;
    }

    public async Task SendMessageAsync<T>(SendMessageDto<T> dto)
    {
        await ConnectIfDisconnected();
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(dto.Topic)
            .WithQualityOfServiceLevel(dto.MqttQualityOfServiceLevel)
            .WithPayload(JsonSerializer.Serialize(dto.Payload))
            .Build();
        await _client.PublishAsync(message);
    }

    public async Task<Guid> SubscribeAsync(string topic, HandleMessageAsyncDelegate messageHandler)
    {
        var factory = new MqttFactory();
        var subscriptionOptions = factory.CreateSubscribeOptionsBuilder().WithTopicFilter(f =>
        {
            f.WithTopic(topic);
            f.WithAtMostOnceQoS();
        }).Build();

        var handlers = new MqttMessageHandlers
        {
            new(topic, messageHandler)
        };

        return await SubscribeAsync(subscriptionOptions, handlers);
    }

    public async Task<Guid> SubscribeAsync(MqttClientSubscribeOptions options, MqttMessageHandlers messageHandlers)
    {
        var objectId = AddMessageHandlers(messageHandlers);
        await ConnectIfDisconnected();
        await _client.SubscribeAsync(options, CancellationToken.None);

        return objectId;
    }

    public async Task Unsubscribe(string topic, Guid objectId)
    {
        _handlers = _handlers.Where(e => e.objectId == objectId && e.topic == topic).ToList();

        if (_handlers.Any(e => e.topic == topic))
            return;

        var options = new MqttFactory()
            .CreateUnsubscribeOptionsBuilder()
            .WithTopicFilter(topic)
            .Build();

        await _client.UnsubscribeAsync(options);
    }

    public async Task Unsubscribe(Guid objectId)
    {
        if (_handlers.Count == 0)
        {
            return;
        }

        var toUnsubscribe = _handlers.Where(e => e.objectId == objectId).ToList();
        _handlers = _handlers.Except(toUnsubscribe).ToList();

        var topicsToUnsubscribe = toUnsubscribe.Select(e => e.topic).ToList();
        var topicsToKeep = _handlers.Select(e => e.topic).ToList();
        topicsToUnsubscribe = topicsToUnsubscribe.Except(topicsToKeep).ToList();

        foreach (var topic in topicsToUnsubscribe)
        {
            await _client.UnsubscribeAsync(topic);
        }
    }

    public async Task ConnectIfDisconnected()
    {
        if (_client.IsConnected)
        {
            return;
        }

        await _client.ConnectAsync(_clientOptions);
    }

    private async Task HandleMessageAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var handlers = GetHandlers(e.ApplicationMessage.Topic);

        await Task.WhenAll(handlers.Select(handler => handler(e)));
    }

    private List<HandleMessageAsyncDelegate> GetHandlers(string topic)
    {
        return _handlers.Where(e => e.topicRegex.IsMatch(topic)).Select(e => e.handler).ToList();
    }

    private Regex TopicToRegex(string topic)
    {
        string topicExp = topic
            .Replace("$", "\\$")
            .Replace("+", "[^/]+")
            .Replace("/#", "(\\$|/.+)");

        return new Regex(topicExp);
    }

    private Guid GenerateObjectId()
    {
        Guid guid;
        do
        {
            guid = Guid.NewGuid();
        } while (_handlers.Any(e => e.objectId == guid));

        return guid;
    }

    private Guid AddMessageHandlers(MqttMessageHandlers messageHandlers)
    {
        if (messageHandlers.Count == 0)
        {
            throw new Exception($"{nameof(messageHandlers)} cannot be empty.");
        }

        if (messageHandlers.Any(e => e.ObjectId != messageHandlers[0].ObjectId))
        {
            throw new Exception($"Each objectId in ${nameof(messageHandlers)} must be identical.");
        }

        var objectId = messageHandlers[0].ObjectId ?? GenerateObjectId();

        foreach (var messageHandler in messageHandlers)
        {
            _handlers.Add((messageHandler.Topic, TopicToRegex(messageHandler.Topic), messageHandler.MessageHandler,
                (Guid)objectId));
        }

        return objectId;
    }
}