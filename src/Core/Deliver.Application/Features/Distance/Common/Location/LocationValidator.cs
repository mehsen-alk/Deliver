using FluentValidation;

namespace Deliver.Application.Features.Distance.Common.Location;

public class LocationValidator : AbstractValidator<Location>
{
    private LocationValidator()
    {
        RuleFor(a => a.Lat).NotEmpty().WithMessage("Lat is required.");

        RuleFor(a => a.Lon).NotEmpty().WithMessage("Lon is required.");
    }
}