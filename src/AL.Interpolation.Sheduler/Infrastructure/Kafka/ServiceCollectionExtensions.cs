using AL.Interpolation.Sheduler.Infrastructure.Kafka.Consumers;
using AL.Interpolation.Sheduler.Infrastructure.Kafka.Consumers.NewTasks;
using AL.Interpolation.Sheduler.Infrastructure.Kafka.Settings;
using Confluent.Kafka;

namespace AL.Interpolation.Sheduler.Infrastructure.Kafka
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKafka(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var kafkaSettings = configuration.GetSection("kafka").Get<KafkaSettings>();

            services.AddConsumer<string, NewTaskDTO, NewTaskConsumerHandler>(
                configuration,
                ConsumerType.NewTask,
                kafkaSettings,
                Deserializers.Utf8,
                new KafkaJsonSerializer<NewTaskDTO>());

            return services;
        }
    }
}
