using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Features.Trips.Common.TripHistoryVm;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Trips.AdminTrips.Query.FilterTrips;

public class FilterTripForAdminQueryHandler
    : IRequestHandler<FilterTripForAdminQuery, List<TripHistoryVm>>
{
    private readonly IMapper _mapper;
    private readonly IAdminTripRepository _adminTripRepository;

    public FilterTripForAdminQueryHandler(
        IAdminTripRepository adminTripRepository,
        IMapper mapper
    )
    {
        _adminTripRepository = adminTripRepository;
        _mapper = mapper;
    }

    public async Task<List<TripHistoryVm>> Handle(
        FilterTripForAdminQuery request,
        CancellationToken cancellationToken
    )
    {
        var trips = await _adminTripRepository.FilterTrip(
            request.Page,
            13,
            request.Id,
            TripStatusExtensions.FromInt(request.Status)
        );

        var tripHistory = _mapper.Map<List<TripHistoryVm>>(trips);

        return tripHistory;
    }
}