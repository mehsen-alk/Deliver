using MediatR;

namespace Deliver.Application.Features.Trips.Query.GetRiderCurrentTrip;

public class GetRiderCurrentTripQuery : IRequest<RiderCurrentTripVm?>
{
    public int RiderId { get; init; }
}