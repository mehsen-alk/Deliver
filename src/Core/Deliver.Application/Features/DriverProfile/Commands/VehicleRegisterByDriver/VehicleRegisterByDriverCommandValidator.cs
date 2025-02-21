using FluentValidation;

namespace Deliver.Application.Features.DriverProfile.Commands.VehicleRegisterByDriver;

public class VehicleRegisterByDriverCommandValidator
    : AbstractValidator<VehicleRegisterByDriverCommand>
{
    public VehicleRegisterByDriverCommandValidator()
    {
        RuleFor(t => t.LicenseImage).NotEmpty().WithMessage("licence image Is Required");

        RuleFor(t => t.VehicleImage).NotEmpty().WithMessage("vehicle image Is Required");
    }
}