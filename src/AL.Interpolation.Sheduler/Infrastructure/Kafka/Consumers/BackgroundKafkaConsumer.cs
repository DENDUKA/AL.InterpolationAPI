using AL.Interpolation.Sheduler.Infrastructure.Kafka.Settings;
using Confluent.Kafka;

namespace AL.Interpolation.Sheduler.Infrastructure.Kafka.Consumers
{
    public class BackgroundKafkaConsumer<TKey, TMessage, THandler> : BackgroundService 
        where THandler: IKafkaConsumerHandler<TKey, TMessage>
    {
        private readonly ILogger<BackgroundKafkaConsumer<TKey, TMessage, THandler>> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly KafkaSettings _kafkaSettings;
        private readonly ConsumerSettings _consumerSettings;
        private readonly IDeserializer<TKey> _keyDeserializer;
        private readonly IDeserializer<TMessage> _messageDeserializer;
        private readonly ConsumerConfig _config;

        public BackgroundKafkaConsumer(
            IServiceProvider serviceProvider,
            KafkaSettings kafkaSettings,
            ConsumerSettings consumerSettings,
            IDeserializer<TKey> keyDeserializer,
            IDeserializer<TMessage> messageDeserializer
            )
        {
            _logger = serviceProvider.GetRequiredService<ILogger<BackgroundKafkaConsumer<TKey, TMessage, THandler>>>();
            _serviceProvider = serviceProvider;
            _kafkaSettings = kafkaSettings;
            _consumerSettings = consumerSettings;
            _keyDeserializer = keyDeserializer;
            _messageDeserializer = messageDeserializer;

            _config = new ConsumerConfig()
            {
                GroupId = consumerSettings.Topic,
                BootstrapServers = kafkaSettings.BoostrapServers,
                EnableAutoCommit = consumerSettings.AutoCommit
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Consume(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error duringkafka consume");
                    await Task.Delay(2000, stoppingToken);
                }
            }
        }

        private async Task Consume(CancellationToken token)
        {
            using var consumer = new ConsumerBuilder<TKey, TMessage>(_config)
                .SetValueDeserializer(_messageDeserializer)
                .SetKeyDeserializer(_keyDeserializer)
                .Build();

            consumer.Subscribe(ConsumerType.NewTask.Name);
            _logger.LogInformation($"Успешно подписались на {ConsumerType.NewTask.Name}");

            while (!token.IsCancellationRequested)
            { 
                var consumed = consumer.Consume(token);
                await _serviceProvider.CreateScope().ServiceProvider
                    .GetRequiredService<THandler>()
                    .Handle(consumed.Message.Key, consumed.Message.Value, token);

                consumer.Commit();
            }
        }
    }
}
