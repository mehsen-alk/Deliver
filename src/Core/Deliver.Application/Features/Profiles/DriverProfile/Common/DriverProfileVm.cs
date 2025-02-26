namespace Deliver.Application.Features.Profiles.DriverProfile.Common;

public class DriverProfileVm
{
    public int UserId { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Phone { get; set; } = string.Empty;
    public string? VehicleImage { get; set; } = null;
    public string? LicenseImage { get; set; } = null;
    public string? ProfileImage { get; set; } = null;
}