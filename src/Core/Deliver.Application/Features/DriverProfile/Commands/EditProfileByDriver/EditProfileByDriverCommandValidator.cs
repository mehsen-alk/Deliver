using FluentValidation;

namespace Deliver.Application.Features.DriverProfile.Commands.EditProfileByDriver;

public class EditProfileByDriverCommandValidator
    : AbstractValidator<EditProfileByDriverCommand>
{
    public EditProfileByDriverCommandValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .WithMessage("Name Is Required")
            .Length(3, 20)
            .WithMessage("Name must be between 3 and 20 characters");

        RuleFor(t => t.LicenseImage).NotEmpty().WithMessage("licence image Is Required");

        RuleFor(t => t.VehicleImage).NotEmpty().WithMessage("vehicle image Is Required");

        RuleFor(t => t.ProfileImage).NotEmpty().WithMessage("profile image Is Required");
    }
}