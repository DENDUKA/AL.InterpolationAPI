namespace AL.Interpolation.Sheduler.Infrastructure.Kafka.Consumers
{
    public class ConsumerType
    {
        public static readonly ConsumerType NewTask = new("NewTask");

        public ConsumerType(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
