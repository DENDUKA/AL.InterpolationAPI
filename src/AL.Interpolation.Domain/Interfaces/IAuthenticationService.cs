namespace AL.Interpolation.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<bool> IsAuthCodeValid(string authCode);

        public Task<bool> IsFreeSlots(string authToken);
    }
}