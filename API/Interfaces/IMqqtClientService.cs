using Models.Delegates.MQTT;
using MQTTnet.Client;
using Models.DTO.MQTT;

namespace Aqua_Sharp_Backend.Interfaces;

public interface IMqqtClientService
{
    Task SendMessageAsync<T>(SendMessageDto<T> dto);
    Task<Guid> SubscribeAsync(string topic, HandleMessageAsyncDelegate messageHandler);
    Task<Guid> SubscribeAsync(MqttClientSubscribeOptions options, MqttMessageHandlers messageHandlers);
    Task Unsubscribe(string topic, Guid objectId);
    Task Unsubscribe(Guid objectId);

    Task ConnectIfDisconnected();
}