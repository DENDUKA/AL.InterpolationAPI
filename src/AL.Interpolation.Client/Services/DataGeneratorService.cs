using AL.Interpolation.Client.DTO;
using AL.Interpolation.Entities.Interpolation;
using AL.Interpolation.Entities.Interpolation.Enums;

namespace AL.Interpolation.Client.Services
{
    public class DataGeneratorService
    {
        public Task<BSplineParametersRequest> BSplineParametersRequest()
        {
            BSplineParametersRequest request = new();
            int pointsCount = Faker.RandomNumber.Next(4, 1000);
            List<Point> points = new();

            for (int i = 3; i < pointsCount; i++)
            {
                points.Add(new Point(
                    Faker.RandomNumber.Next(-10000, 10000) * 0.99D,
                    Faker.RandomNumber.Next(-10000, 10000) * 0.99D,
                    Faker.RandomNumber.Next(-10000, 10000) * 0.99D
                    ));
            }

            request.Points = points;
            request.M0 = 1;
            request.N0 = 1;
            request.H = 7;
            request.CountIterations = 50;
            request.TypeOfWorkBSpline = ETypeOfWorkBSpline.Local;
            request.InterpolationParameters = new InterpolationParameters()
            {
                CellSizeX = 10,
                CellSizeY = 10,
                StepX = Faker.RandomNumber.Next(10, 300),
                StepY = Faker.RandomNumber.Next(10, 300),
            };


            return Task.FromResult(request);
        }
    }
}
