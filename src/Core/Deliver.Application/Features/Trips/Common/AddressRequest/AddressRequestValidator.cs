using FluentValidation;

namespace Deliver.Application.Features.Trips.Common.AddressRequest;

public class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    public AddressRequestValidator()
    {
        RuleFor(a => a.Latitude).NotEmpty().WithMessage("Latitude is required.");

        RuleFor(a => a.Longitude).NotEmpty().WithMessage("Longitude is required.");
    }
}