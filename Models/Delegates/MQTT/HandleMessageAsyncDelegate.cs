using MQTTnet.Client;

namespace Models.Delegates.MQTT;

public delegate Task HandleMessageAsyncDelegate(MqttApplicationMessageReceivedEventArgs e);