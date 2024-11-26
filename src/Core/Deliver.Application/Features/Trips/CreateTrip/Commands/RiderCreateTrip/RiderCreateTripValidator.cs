using Deliver.Application.Features.Trips.Common.AddressRequest;
using FluentValidation;

namespace Deliver.Application.Features.Trips.CreateTrip.Commands.RiderCreateTrip;

public class RiderCreateTripValidator : AbstractValidator<RiderCreateTripCommand>
{
    public RiderCreateTripValidator()
    {
        RuleFor(t => t.Distance).GreaterThan(0);

        RuleFor(t => t.Duration).NotEmpty();

        RuleFor(t => t.PickUpAddress)
            .NotEmpty()
            .WithMessage("Please specify a valid Pick Up Address")
            .SetValidator(new AddressRequestValidator());

        RuleFor(t => t.DropOfAddress)
            .NotEmpty()
            .WithMessage("Please specify a valid Drop Of Address")
            .SetValidator(new AddressRequestValidator());
    }
}