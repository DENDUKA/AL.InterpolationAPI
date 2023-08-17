namespace AL.Interpolation.API.Servises
{
    public interface IInterpolationTaskRegistrationService
    {
        Task<string> RegistrationBSplineTask(string authToken);
    }
}