using Models.Delegates.MQTT;
 
namespace Models.DTO.MQTT;

public record SubscriptionDto (string Topic, HandleMessageAsyncDelegate MessageHandler, Guid? ObjectId = null);