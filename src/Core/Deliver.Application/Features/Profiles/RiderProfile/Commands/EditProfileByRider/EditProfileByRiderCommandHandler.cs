using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Profiles.RiderProfile.Commands.EditProfileByRider;

public class EditProfileByRiderCommandHandler
    : IRequestHandler<EditProfileByRiderCommand, bool>
{
    private readonly IMapper _mapper;

    private readonly IAsyncRepository<Domain.Entities.RiderProfile>
        _riderProfileRepository;

    public EditProfileByRiderCommandHandler(
        IAsyncRepository<Domain.Entities.RiderProfile> riderProfileRepository,
        IMapper mapper
    )
    {
        _riderProfileRepository = riderProfileRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(
        EditProfileByRiderCommand command,
        CancellationToken cancellationToken
    )
    {
        var validator = new EditProfileByRiderCommandValidator();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var tr = await _riderProfileRepository.BeginTransactionAsync();

        var oldProfile = await _riderProfileRepository.GetByIdAsync(command.ProfileId);

        if (oldProfile == null) throw new NotFoundException("Profile not found");

        oldProfile.Status = ProfileStatus.Old;

        await _riderProfileRepository.UpdateAsync(oldProfile);

        var newProfile = _mapper.Map<Domain.Entities.RiderProfile>(command);

        newProfile.UserId = oldProfile.UserId;
        newProfile.Status = ProfileStatus.Current;

        await _riderProfileRepository.AddAsync(newProfile);

        await tr.CommitAsync(cancellationToken);

        return true;
    }
}