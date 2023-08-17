using AL.Interpolation.API.DAL.Repositories.Interfaces;

namespace AL.Interpolation.API.Servises
{
    public class InterpolationTaskRegistrationService : IInterpolationTaskRegistrationService
    {
        private readonly ILogger<InterpolationTaskRegistrationService> _logger;
        private readonly IInterpolationRequestsRepository _interpolationRequestsRepository;

        public InterpolationTaskRegistrationService(
            ILogger<InterpolationTaskRegistrationService> logger,
            IInterpolationRequestsRepository interpolationRequestsRepository) 
        {
            _logger = logger;
            _interpolationRequestsRepository = interpolationRequestsRepository;
        }
        public async Task<string> RegistrationBSplineTask(string authToken)
        {
            var taskId = Guid.NewGuid().ToString();

            await _interpolationRequestsRepository.Increment(authToken, CancellationToken.None);

            var slotsInfo = await _interpolationRequestsRepository.Find(authToken, CancellationToken.None);

            _logger.LogInformation($"{authToken} : {slotsInfo.OcupiedSlots}");

            _logger.LogInformation($"Зарегистрирована новая задача {taskId}");

            return taskId;
        }
    }
}