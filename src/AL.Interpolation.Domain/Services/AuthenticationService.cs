using AL.Interpolation.Domain.Interfaces;

namespace AL.Interpolation.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        { 

        }

        public async Task<bool> IsAuthCodeValid(string authCode)
        {
            return true;
        }

        public async Task<bool> IsFreeSlots(string authCode)
        {
            return true;
        }
    }
}
