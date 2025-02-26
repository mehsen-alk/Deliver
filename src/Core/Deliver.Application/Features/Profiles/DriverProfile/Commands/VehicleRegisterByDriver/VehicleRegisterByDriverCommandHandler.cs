using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using MediatR;

namespace Deliver.Application.Features.Profiles.DriverProfile.Commands.
    VehicleRegisterByDriver;

public class VehicleRegisterByDriverCommandHandler
    : IRequestHandler<VehicleRegisterByDriverCommand, bool>
{
    private readonly IAsyncRepository<Domain.Entities.DriverProfile>
        _driverProfileRepository;

    public VehicleRegisterByDriverCommandHandler(
        IAsyncRepository<Domain.Entities.DriverProfile> driverProfileRepository
    )
    {
        _driverProfileRepository = driverProfileRepository;
    }

    public async Task<bool> Handle(
        VehicleRegisterByDriverCommand command,
        CancellationToken cancellationToken
    )
    {
        var validator = new VehicleRegisterByDriverCommandValidator();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var driverProfile =
            await _driverProfileRepository.GetByIdAsync(command.ProfileId);

        if (driverProfile == null) throw new NotFoundException("Profile not found");

        driverProfile.LicenseImage = command.LicenseImage;
        driverProfile.VehicleImage = command.VehicleImage;

        await _driverProfileRepository.UpdateAsync(driverProfile);

        return true;
    }
}