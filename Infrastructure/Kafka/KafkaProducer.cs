using Confluent.Kafka;
using System.Text.Json;
using PagosCQRSDDDEventSourcing.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace PagosCQRSDDDEventSourcing.Infrastructure.Kafka;

public class KafkaProducer: IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic = "Pagos";

    public KafkaProducer(IConfiguration configuration)
    {
        var kafkaSettings = configuration.GetSection("KafkaSettings");

        var config = new ProducerConfig
        {
            BootstrapServers = kafkaSettings["Broker"],
            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.Plain,
            SaslUsername = kafkaSettings["SaslUsername"],
            SaslPassword = kafkaSettings["SaslPassword"],
            Acks = Acks.All,
            MessageTimeoutMs = 10000, // evita el timeout largo por defecto (60000ms)
            RetryBackoffMs = 500
        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }


    public async Task SendMessageAsync(object message)
    {
        var value = JsonSerializer.Serialize(message);
        await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = value });
    }
}
