using MQTTnet.Protocol;

namespace Models.DTO.MQTT;

public record SendMessageDto<T>(string Topic, T Payload, MqttQualityOfServiceLevel MqttQualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce);