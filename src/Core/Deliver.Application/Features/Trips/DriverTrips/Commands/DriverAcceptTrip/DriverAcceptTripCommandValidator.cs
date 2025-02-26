using Deliver.Application.Features.Trips.Common.AddressRequest;
using FluentValidation;

namespace Deliver.Application.Features.Trips.DriverTrips.Commands.DriverAcceptTrip;

public class DriverAcceptTripCommandValidator : AbstractValidator<DriverAcceptTripCommand>
{
    public DriverAcceptTripCommandValidator()
    {
        RuleFor(t => t.TripId).NotEmpty().WithMessage("Trip Id Is Required");

        RuleFor(t => t.DriverAddress)
            .NotEmpty()
            .WithMessage("Please specify a valid Address")
            .SetValidator(new AddressRequestValidator());
    }
}