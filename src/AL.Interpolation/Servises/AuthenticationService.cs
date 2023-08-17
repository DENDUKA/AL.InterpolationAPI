using AL.Interpolation.API.DAL.Repositories.Interfaces;

namespace AL.Interpolation.API.Servises
{
    public class AuthenticationService : IAuthenticationService
    {
        private IInterpolationRequestsRepository _interpolationRequestsRepository;

        public AuthenticationService(IInterpolationRequestsRepository interpolationRequestsRepository)
        {
            _interpolationRequestsRepository = interpolationRequestsRepository;
        }

        public async Task<bool> IsAuthCodeValid(string authCode)
        {
            if (!await _interpolationRequestsRepository.IsExist(authCode, CancellationToken.None))
            {
                await _interpolationRequestsRepository.Insert(
                    authCode,
                    new Entities.Redis.RedisSlotsInfo() { OcupiedSlots = 0 },
                    CancellationToken.None);
            }

            return true;
        }

        public async Task<bool> IsFreeSlots(string authCode)
        {
            var slotsInfo = await _interpolationRequestsRepository.Find(authCode, CancellationToken.None);
            return slotsInfo?.OcupiedSlots < 5;
        }
    }
}
