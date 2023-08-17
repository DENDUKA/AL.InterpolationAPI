namespace AL.Interpolation.Client.Services
{
    public class InterpolationStatusCheckerHostedSevice : IHostedService
    {
        private readonly ILogger<InterpolationStatusCheckerHostedSevice> _logger;
        private Timer? _timer = null;

        public InterpolationStatusCheckerHostedSevice(ILogger<InterpolationStatusCheckerHostedSevice> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Data Generator Hosted Service running.");

            _timer = new Timer(StatusChecker, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void StatusChecker(object? state)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
