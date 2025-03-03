namespace Deliver.Application.Features.Profiles.RiderProfile.Commands.EditProfileByRider;

public class EditProfileByRiderRequest
{
    public required string Name { get; set; } = string.Empty;
    public required string Phone { get; set; } = string.Empty;
    public string? ProfileImage { get; set; } = null;
}