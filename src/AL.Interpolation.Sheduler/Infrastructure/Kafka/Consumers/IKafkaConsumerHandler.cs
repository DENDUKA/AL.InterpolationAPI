namespace AL.Interpolation.Sheduler.Infrastructure.Kafka.Consumers
{
    public interface IKafkaConsumerHandler<TKey, TValue>
    {
        public Task Handle(TKey key, TValue message, CancellationToken token);
    }
}
