namespace Deliver.Application.Features.Profiles.RiderProfile.Commands.EditProfileByRider;

public class EditProfileByRiderRequest
{
    public required string Name { get; set; } = string.Empty;
    public required string Phone { get; set; } = string.Empty;
    public string? VehicleImage { get; set; } = null;
    public string? LicenseImage { get; set; } = null;
    public string? ProfileImage { get; set; } = null;
}