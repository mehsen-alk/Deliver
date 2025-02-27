using Deliver.Application.Features.Trips.Common.TripHistoryVm;
using MediatR;

namespace Deliver.Application.Features.Trips.RiderTrips.Query.GetTripHistory;

public class GetRiderTripHistoryQuery : IRequest<List<TripHistoryVm>>
{
    public int RiderId { get; set; }
    public int Page { get; set; }
}