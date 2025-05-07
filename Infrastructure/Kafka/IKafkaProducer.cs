namespace PagosCQRSDDDEventSourcing.Infrastructure.Kafka;

public interface IKafkaProducer
{
    Task SendMessageAsync(object message);
}