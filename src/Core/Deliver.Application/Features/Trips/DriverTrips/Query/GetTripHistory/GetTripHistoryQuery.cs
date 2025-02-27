using Deliver.Application.Features.Trips.Common.TripHistoryVm;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Query.GetTripHistory;

public class GetDriverTripHistoryQuery : IRequest<List<TripHistoryVm>>
{
    public int DriverId { get; set; }
    public int Page { get; set; }
}