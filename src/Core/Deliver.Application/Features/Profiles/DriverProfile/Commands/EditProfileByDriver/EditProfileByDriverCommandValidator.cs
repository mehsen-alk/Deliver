using Deliver.Application.Validations;
using FluentValidation;

namespace Deliver.Application.Features.Profiles.DriverProfile.Commands.
    EditProfileByDriver;

public class EditProfileByDriverCommandValidator
    : AbstractValidator<EditProfileByDriverCommand>
{
    public EditProfileByDriverCommandValidator()
    {
        RuleFor(t => t.Name).ValidName();

        RuleFor(t => t.LicenseImage).NotEmpty().WithMessage("licence image Is Required");

        RuleFor(t => t.VehicleImage).NotEmpty().WithMessage("vehicle image Is Required");

        RuleFor(t => t.ProfileImage).NotEmpty().WithMessage("profile image Is Required");
    }
}