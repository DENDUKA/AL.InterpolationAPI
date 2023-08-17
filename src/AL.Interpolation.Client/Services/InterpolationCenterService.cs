using AL.Interpolation.Client.DTO;

namespace AL.Interpolation.Client.Services
{
    public class InterpolationCenterService
    {
        private readonly HashSet<string> _taskInWork = new();
        private readonly ILogger<InterpolationCenterService> _logger;
        private readonly InterpolationAPIService _interpolationAPIService;

        public InterpolationCenterService(
            ILogger<InterpolationCenterService> logger,
            InterpolationAPIService interpolationAPIService)
        {
            _logger = logger;
            _interpolationAPIService = interpolationAPIService;
        }

        public async Task StartNewBSpline(BSplineParametersRequest request)
        {
            var taskId = await _interpolationAPIService.PostBSpline(request);

            _taskInWork.Add(taskId);
        }

        public async Task CompletedBSpline()
        { 

        }
    }
}