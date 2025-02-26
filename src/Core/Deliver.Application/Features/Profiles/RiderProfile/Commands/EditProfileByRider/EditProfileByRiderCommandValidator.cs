using FluentValidation;

namespace Deliver.Application.Features.Profiles.RiderProfile.Commands.EditProfileByRider;

public class EditProfileByRiderCommandValidator
    : AbstractValidator<EditProfileByRiderCommand>
{
    public EditProfileByRiderCommandValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .WithMessage("Name Is Required")
            .Length(3, 20)
            .WithMessage("Name must be between 3 and 20 characters");

        RuleFor(t => t.ProfileImage).NotEmpty().WithMessage("profile image Is Required");
    }
}