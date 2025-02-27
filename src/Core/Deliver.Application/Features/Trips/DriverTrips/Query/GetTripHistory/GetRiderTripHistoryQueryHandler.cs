using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Features.Trips.Common.TripHistoryVm;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Query.GetTripHistory;

public class GetDriverTripHistoryQueryHandler
    : IRequestHandler<GetDriverTripHistoryQuery, List<TripHistoryVm>>
{
    private readonly IDriverTripRepository _driverTripRepository;
    private readonly IMapper _mapper;

    public GetDriverTripHistoryQueryHandler(
        IDriverTripRepository driverTripRepository,
        IMapper mapper
    )
    {
        _driverTripRepository = driverTripRepository;
        _mapper = mapper;
    }

    public async Task<List<TripHistoryVm>> Handle(
        GetDriverTripHistoryQuery request,
        CancellationToken cancellationToken
    )
    {
        var trips = await _driverTripRepository.GetDriverTrips(
            request.DriverId,
            request.Page,
            13
        );

        var tripHistory = _mapper.Map<List<TripHistoryVm>>(trips);

        return tripHistory;
    }
}