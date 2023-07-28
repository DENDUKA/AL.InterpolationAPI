using AL.Interpolation.API.DTO;
using AL.Interpolation.API.Extensions;
using AL.Interpolation.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AL.Interpolation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterpolationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IInterpolationService _interpolationService;
        private readonly ILogger _logger;

        public InterpolationController(
            IAuthenticationService authenticationService, 
            IInterpolationService interpolationService,
            ILogger logger)
        {
            _authenticationService = authenticationService;
            _interpolationService = interpolationService;
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

            if (!await responce.Validate())
            {
                return BadRequest($"Входные данные не валидны");
            }

            var id = Guid.NewGuid().ToString();

            _logger.LogDebug($"Зарегистрирована новая задача {id}");

            return Ok(id);
        }
    }
}
