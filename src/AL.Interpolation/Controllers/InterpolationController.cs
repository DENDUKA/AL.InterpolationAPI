using AL.Interpolation.API.DTO;
using AL.Interpolation.API.DTO.Validators;
using AL.Interpolation.API.Servises;
using AL.Interpolation.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AL.Interpolation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterpolationController : Controller
    {
        private readonly IInterpolationTaskRegistrationService _interpolationTaskRegistrationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IInterpolationService _interpolationService;
        private readonly ILogger<InterpolationController> _logger;
        private readonly BSplineParametersResponceValidator _bSplineParametersValidator;

        public InterpolationController(
            IAuthenticationService authenticationService, 
            IInterpolationService interpolationService,
            BSplineParametersResponceValidator bSplineParametersValidator,
            ILogger<InterpolationController> logger,
            IInterpolationTaskRegistrationService interpolationTaskRegistrationService)
        {
            _interpolationTaskRegistrationService = interpolationTaskRegistrationService;
            _authenticationService = authenticationService;
            _interpolationService = interpolationService;
            _bSplineParametersValidator = bSplineParametersValidator;
            _logger = logger;
        }

        [HttpPost(Name = "BSpline")]
        public async Task<IActionResult> BSpline(string authToken, [FromBody]BSplineParametersResponce responce)
        {
            if (!await _authenticationService.IsAuthCodeValid(authToken))
            {
                return Unauthorized($"Не удалось аавторизовать по токену {authToken}"); 
            }

            if (!await _authenticationService.IsFreeSlots(authToken))
            {
                return Unauthorized($"Нет свободных слотов");
            }

            if (!(await _bSplineParametersValidator.ValidateAsync(responce)).IsValid)
            {
                return BadRequest($"Входные данные не валидны");
            }

            var taskId = await _interpolationTaskRegistrationService.RegistrationBSplineTask(authToken);

            return Ok(taskId);
        }
    }
} 
