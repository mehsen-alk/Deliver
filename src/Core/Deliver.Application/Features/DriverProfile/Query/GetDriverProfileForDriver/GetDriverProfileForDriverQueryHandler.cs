using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Features.DriverProfile.Common;
using MediatR;

namespace Deliver.Application.Features.DriverProfile.Query.GetDriverProfileForDriver;

public class GetDriverProfileForDriverQueryHandler
    : IRequestHandler<GetDriverProfileForDriverQuery, DriverProfileVm>
{
    private readonly IDriverProfileRepository _driverProfileRepository;

    private readonly IMapper _mapper;

    public GetDriverProfileForDriverQueryHandler(
        IMapper mapper,
        IDriverProfileRepository driverProfileRepository
    )
    {
        _mapper = mapper;
        _driverProfileRepository = driverProfileRepository;
    }

    public async Task<DriverProfileVm> Handle(
        GetDriverProfileForDriverQuery request,
        CancellationToken cancellationToken
    )
    {
        var driverProfile =
            await _driverProfileRepository.GetDriverCurrentProfile(request.UserId);

        return _mapper.Map<DriverProfileVm>(driverProfile);
    }
}