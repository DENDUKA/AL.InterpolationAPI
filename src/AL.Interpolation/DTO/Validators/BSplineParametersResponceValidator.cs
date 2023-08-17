using FluentValidation;

namespace AL.Interpolation.API.DTO.Validators
{
    public class BSplineParametersResponceValidator : AbstractValidator<BSplineParametersResponce>
    {
        public BSplineParametersResponceValidator()
        {
            RuleFor(x=>x.H).GreaterThan(0);
            RuleFor(x => x.M0).GreaterThan(0);
            RuleFor(x => x.N0).GreaterThan(0);
            RuleFor(x => x.CountIterations).GreaterThan(0);

            RuleFor(x => x.InterpolationParameters.StepX).GreaterThan(0);
            RuleFor(x => x.InterpolationParameters.StepY).GreaterThan(0);
            RuleFor(x => x.InterpolationParameters.CellSizeX).GreaterThan(0);
            RuleFor(x => x.InterpolationParameters.CellSizeY).GreaterThan(0);

            RuleFor(x => x.Points.Count()).GreaterThan(2);

            RuleFor(x => x.TypeOfWorkBSpline).IsInEnum();
        }
    }
}
