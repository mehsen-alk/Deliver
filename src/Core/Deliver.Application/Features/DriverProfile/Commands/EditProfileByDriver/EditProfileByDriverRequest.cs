namespace Deliver.Application.Features.DriverProfile.Commands.EditProfileByDriver;

public class EditProfileByDriverRequest
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? VehicleImage { get; set; } = null;
    public string? LicenseImage { get; set; } = null;
    public string? ProfileImage { get; set; } = null;
}