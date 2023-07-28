using AL.Interpolation.API.DTO;

namespace AL.Interpolation.API.Extensions
{
    public static class RequestExtensions
    {
        public static async Task<bool> Validate(this BSplineParametersResponce responce)
        {
            if (responce.H <= 0 || 
                responce.M0 <= 0 || 
                responce.N0 <= 0 ||
                responce.CountIterations <= 0)
            { 
                return false;
            }

            if (responce.InterpolationParameters.StepY <= 0 ||
                responce.InterpolationParameters.StepX <= 0 ||
                responce.InterpolationParameters.CellSizeY <= 0 ||
                responce.InterpolationParameters.CellSizeX <= 0)
            { 
                return false;
            }

            if (responce.Points.Count() <= 2)
            {
                return false;
            }

            if (!Enum.IsDefined(responce.TypeOfWorkBSpline))
            {
                return false;
            }

            return true;
        }
    }
}
