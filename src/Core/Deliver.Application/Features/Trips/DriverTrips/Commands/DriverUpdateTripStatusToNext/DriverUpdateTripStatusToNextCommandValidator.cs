using Deliver.Application.Features.Trips.Common.AddressRequest;
using FluentValidation;

namespace Deliver.Application.Features.Trips.DriverTrips.Commands.
    DriverUpdateTripStatusToNext;

public class DriverUpdateTripStatusToNextCommandValidator
    : AbstractValidator<DriverUpdateTripStatusToNextCommand>
{
    public DriverUpdateTripStatusToNextCommandValidator()
    {
        RuleFor(t => t.DriverAddress)
            .NotEmpty()
            .WithMessage("Please specify driver address")
            .SetValidator(new AddressRequestValidator());
    }
}