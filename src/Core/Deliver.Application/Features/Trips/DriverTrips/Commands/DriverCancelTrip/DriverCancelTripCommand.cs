using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Commands.DriverCancelTrip;

public class DriverCancelTripCommand : IRequest<int>
{
    public int UserId { get; set; }
}