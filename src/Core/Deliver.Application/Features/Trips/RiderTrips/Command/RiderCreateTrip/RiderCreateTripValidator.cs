using Deliver.Application.Features.Trips.Common.AddressRequest;
using FluentValidation;

namespace Deliver.Application.Features.Trips.RiderTrips.Command.RiderCreateTrip;

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

        RuleFor(t => t.DropOffAddress)
            .NotEmpty()
            .WithMessage("Please specify a valid Drop Off Address")
            .SetValidator(new AddressRequestValidator());
    }
}