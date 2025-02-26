using MediatR;

namespace Deliver.Application.Features.Trips.RiderTrips.Command.RiderCancelTrip;

public class RiderCancelTripCommand : IRequest<int>
{
    public int UserId { get; set; }
}