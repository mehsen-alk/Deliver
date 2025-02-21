using MediatR;

namespace Deliver.Application.Features.DriverProfile.Commands.VehicleRegisterByDriver;

public class VehicleRegisterByDriverCommand : IRequest<bool>
{
    public required int ProfileId { get; set; }
    public required string VehicleImage { get; set; } = string.Empty;
    public required string LicenseImage { get; set; } = string.Empty;
}