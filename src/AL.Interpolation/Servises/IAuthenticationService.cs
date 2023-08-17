namespace AL.Interpolation.API.Servises
{
    public interface IAuthenticationService
    {
        public Task<bool> IsAuthCodeValid(string authCode);

        public Task<bool> IsFreeSlots(string authCode);
    }
}
