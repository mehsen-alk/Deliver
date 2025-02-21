using MediatR;

namespace Deliver.Application.Features.DriverProfile.Commands.EditProfileByDriver;

public class EditProfileByDriverCommand : IRequest<bool>
{
    public required int UserId { get; set; }
    public required int ProfileId { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Phone { get; set; } = string.Empty;
    public string? VehicleImage { get; set; } = null;
    public string? LicenseImage { get; set; } = null;
    public string? ProfileImage { get; set; } = null;
}