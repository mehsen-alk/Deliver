using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverCurrentTrip;

public class GetDriverCurrentTripQueryHandler
    : IRequestHandler<GetDriverCurrentTripQuery, DriverCurrentTripVm?>

{
    private readonly IDriverTripRepository _driverTripRepository;
    private readonly IMapper _mapper;

    public GetDriverCurrentTripQueryHandler(
        IMapper mapper,
        IDriverTripRepository driverTripRepository
    )
    {
        _mapper = mapper;
        _driverTripRepository = driverTripRepository;
    }

    public async Task<DriverCurrentTripVm?> Handle(
        GetDriverCurrentTripQuery request,
        CancellationToken cancellationToken
    )
    {
        var trip = await _driverTripRepository.GetCurrentTripAsync(request.DriverId);

        var vm = _mapper.Map<DriverCurrentTripVm>(trip);

        return vm;
    }
}