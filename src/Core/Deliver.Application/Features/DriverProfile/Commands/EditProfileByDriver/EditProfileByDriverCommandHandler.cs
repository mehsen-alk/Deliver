using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.DriverProfile.Commands.EditProfileByDriver;

public class EditProfileByDriverCommandHandler
    : IRequestHandler<EditProfileByDriverCommand, bool>
{
    private readonly IAsyncRepository<Domain.Entities.DriverProfile>
        _driverProfileRepository;

    private readonly IMapper _mapper;

    public EditProfileByDriverCommandHandler(
        IAsyncRepository<Domain.Entities.DriverProfile> driverProfileRepository,
        IMapper mapper
    )
    {
        _driverProfileRepository = driverProfileRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(
        EditProfileByDriverCommand command,
        CancellationToken cancellationToken
    )
    {
        var validator = new EditProfileByDriverCommandValidator();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var tr = await _driverProfileRepository.BeginTransactionAsync();

        var oldProfile = await _driverProfileRepository.GetByIdAsync(command.ProfileId);

        if (oldProfile == null) throw new NotFoundException("Profile not found");

        oldProfile.Status = ProfileStatus.Old;

        await _driverProfileRepository.UpdateAsync(oldProfile);

        var newProfile = _mapper.Map<Domain.Entities.DriverProfile>(command);

        newProfile.UserId = oldProfile.UserId;
        newProfile.CurrentLocationId = oldProfile.CurrentLocationId;
        newProfile.Status = ProfileStatus.Current;

        await _driverProfileRepository.AddAsync(newProfile);

        await tr.CommitAsync(cancellationToken);

        return true;
    }
}