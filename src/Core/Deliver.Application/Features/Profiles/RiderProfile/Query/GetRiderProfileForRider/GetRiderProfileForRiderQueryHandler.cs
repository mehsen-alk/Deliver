using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Features.Profiles.RiderProfile.Common;
using MediatR;

namespace Deliver.Application.Features.Profiles.RiderProfile.Query.
    GetRiderProfileForRider;

public class GetRiderProfileForRiderQueryHandler
    : IRequestHandler<GetRiderProfileForRiderQuery, RiderProfileVm>
{
    private readonly IMapper _mapper;
    private readonly IRiderProfileRepository _riderProfileRepository;

    public GetRiderProfileForRiderQueryHandler(
        IMapper mapper,
        IRiderProfileRepository riderProfileRepository
    )
    {
        _mapper = mapper;
        _riderProfileRepository = riderProfileRepository;
    }

    public async Task<RiderProfileVm> Handle(
        GetRiderProfileForRiderQuery request,
        CancellationToken cancellationToken
    )
    {
        var riderProfile =
            await _riderProfileRepository.GetRiderCurrentProfile(request.UserId);

        return _mapper.Map<RiderProfileVm>(riderProfile);
    }
}