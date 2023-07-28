using AL.Interpolation.Entities.Interpolation;
using AL.Interpolation.Entities.Interpolation.Enums;

namespace AL.Interpolation.Domain.Interfaces
{
    public interface IInterpolationService
    {
        public Task<Surface> BSpline(
            IEnumerable<Point> points,
            int m0,
            int n0,
            int h,
            int countIterations,
            ETypeOfWorkBSpline typeOfWorkBSpline,
            InterpolationParameters interpolationParameters);
    }
}
