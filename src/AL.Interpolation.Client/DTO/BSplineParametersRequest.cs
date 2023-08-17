using AL.Interpolation.Entities.Interpolation;
using AL.Interpolation.Entities.Interpolation.Enums;

namespace AL.Interpolation.Client.DTO
{
    public record BSplineParametersRequest
    {
        public IEnumerable<Point> Points { get; set; }
        public int M0 { get; set; }
        public int N0 { get; set; }
        public int H { get; set; }
        public int CountIterations { get; set; }
        public ETypeOfWorkBSpline TypeOfWorkBSpline { get; set; }
        public InterpolationParameters InterpolationParameters { get; set; }
    }
}
