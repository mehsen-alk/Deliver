namespace Deliver.Application.Features.DriverProfile.Commands.EditProfileByDriver;

public class EditProfileByDriverRequest
{
    public required string Name { get; set; } = string.Empty;
    public required string Phone { get; set; } = string.Empty;
    public string? VehicleImage { get; set; } = null;
    public string? LicenseImage { get; set; } = null;
    public string? ProfileImage { get; set; } = null;
}