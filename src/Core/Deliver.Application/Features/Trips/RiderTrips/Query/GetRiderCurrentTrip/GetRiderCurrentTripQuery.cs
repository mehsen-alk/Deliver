using MediatR;

namespace Deliver.Application.Features.Trips.RiderTrips.Query.GetRiderCurrentTrip;

public class GetRiderCurrentTripQuery : IRequest<RiderCurrentTripVm?>
{
    public int RiderId { get; init; }
}