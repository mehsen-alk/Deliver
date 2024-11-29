using FluentValidation;

namespace Deliver.Application.Features.Distance.Query.GetDistance;

public class GetDistanceQueryValidator : AbstractValidator<GetDistanceQuery>
{
    public GetDistanceQueryValidator()
    {
        RuleFor(a => a.Origin)
            .NotNull()
            .WithMessage("Origin is required")
            .NotEmpty()
            .WithMessage("Origin cannot be empty");

        RuleFor(a => a.Destination)
            .NotNull()
            .WithMessage("Destination is required")
            .NotEmpty()
            .WithMessage("Destination cannot be empty");
    }
}