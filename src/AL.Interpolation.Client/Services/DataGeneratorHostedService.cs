namespace AL.Interpolation.Client.Services
{
    public class DataGeneratorHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<DataGeneratorHostedService> _logger;
        private readonly DataGeneratorService _dataGeneratorService;
        private Timer? _timer = null;
        private readonly Random _random = new Random();
        private readonly InterpolationCenterService _interpolationCenterService;

        public DataGeneratorHostedService(
            ILogger<DataGeneratorHostedService> logger,
            DataGeneratorService dataGeneratorService,
            InterpolationCenterService interpolationCenterService)
        {
            _logger = logger;
            _dataGeneratorService = dataGeneratorService;
            _interpolationCenterService = interpolationCenterService;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Data Generator Hosted Service running.");

            _timer = new Timer(Generate, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        private async void Generate(object? state)
        {
            if (_random.Next(0, 100) < 90)
            {
                return;
            }

            _logger.LogInformation($"Generation new request");

            var bsplineRequest = await _dataGeneratorService.BSplineParametersRequest();

            await _interpolationCenterService.StartNewBSpline(bsplineRequest);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
