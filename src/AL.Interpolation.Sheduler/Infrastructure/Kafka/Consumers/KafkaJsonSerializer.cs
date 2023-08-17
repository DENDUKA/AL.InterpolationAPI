using Confluent.Kafka;
using System.Text.Json;

namespace AL.Interpolation.Sheduler.Infrastructure.Kafka.Consumers
{
    public class KafkaJsonSerializer<TValue> : IDeserializer<TValue>
    {
        public TValue Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) =>
             JsonSerializer.Deserialize<TValue>(data)!;
    }
}