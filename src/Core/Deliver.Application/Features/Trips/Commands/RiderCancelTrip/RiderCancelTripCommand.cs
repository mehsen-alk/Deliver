using MediatR;

namespace Deliver.Application.Features.Trips.Commands.RiderCancelTrip;

public class RiderCancelTripCommand : IRequest<int>
{
    public int UserId { get; set; }
}