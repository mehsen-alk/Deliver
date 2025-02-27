using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Features.Trips.Common.TripHistoryVm;
using MediatR;

namespace Deliver.Application.Features.Trips.RiderTrips.Query.GetTripHistory;

public class GetRiderTripHistoryQueryHandler
    : IRequestHandler<GetRiderTripHistoryQuery, List<TripHistoryVm>>
{
    private readonly IMapper _mapper;
    private readonly IRiderTripRepository _riderTripRepository;

    public GetRiderTripHistoryQueryHandler(
        IRiderTripRepository riderTripRepository,
        IMapper mapper
    )
    {
        _riderTripRepository = riderTripRepository;
        _mapper = mapper;
    }

    public async Task<List<TripHistoryVm>> Handle(
        GetRiderTripHistoryQuery request,
        CancellationToken cancellationToken
    )
    {
        var trips = await _riderTripRepository.GetRiderTrips(
            request.RiderId,
            request.Page,
            13
        );

        var tripHistory = _mapper.Map<List<TripHistoryVm>>(trips);

        return tripHistory;
    }
}