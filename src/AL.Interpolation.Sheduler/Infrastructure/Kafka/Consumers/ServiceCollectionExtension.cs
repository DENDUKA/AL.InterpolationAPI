using AL.Interpolation.Sheduler.Infrastructure.Kafka.Settings;
using Confluent.Kafka;

namespace AL.Interpolation.Sheduler.Infrastructure.Kafka.Consumers
{
    public static partial class ServiceCollectionExtension
    {
        public static IServiceCollection AddConsumer<TKey, TMessage, THandler>(
            this IServiceCollection services,
            IConfiguration configuration,
            ConsumerType consumerType,
            KafkaSettings kafkaSettings,
            IDeserializer<TKey> keyDeserializer,
            IDeserializer<TMessage> valueDeserializer) where THandler : class, IKafkaConsumerHandler<TKey, TMessage> 
        {
            var consumerSettings = configuration
                .GetSection($"Kafka:Consumers:{consumerType.Name}")
                .Get<ConsumerSettings>();

            services.AddHostedService(
                sp => new BackgroundKafkaConsumer<TKey, TMessage, THandler>(
                    sp,
                    kafkaSettings,
                    consumerSettings,
                    keyDeserializer,
                    valueDeserializer));

            services.AddScoped<THandler>();

            return services;
        }
    }
}
