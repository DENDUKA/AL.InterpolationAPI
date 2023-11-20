namespace AL.Interpolation.Sheduler.Infrastructure.Kafka.Consumers.NewTasks
{
    public class NewTaskConsumerHandler : IKafkaConsumerHandler<string, NewTaskDTO>
    {
        public Task Handle(string key, NewTaskDTO message, CancellationToken token)
        {
            
            return Task.FromResult(0);
        }
    }
}
