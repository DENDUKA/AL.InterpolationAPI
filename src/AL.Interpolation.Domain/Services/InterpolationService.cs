using AL.Interpolation.Domain.Interfaces;
using AL.Interpolation.Entities.Interpolation;
using AL.Interpolation.Entities.Interpolation.Enums;

namespace AL.Interpolation.Domain.Services
{
    public class InterpolationService : IInterpolationService
    {
        public async Task<Surface> BSpline(
            IEnumerable<Point> points,
            int m0,
            int n0,
            int h,
            int countIterations,
            ETypeOfWorkBSpline typeOfWorkBSpline,
            InterpolationParameters interpolationParameters)
        {
            return new Surface();
        }
    }
}