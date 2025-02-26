using Deliver.Application.Features.Trips.Common.AddressRequest;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Commands.DriverAcceptTrip;

public class DriverAcceptTripCommand : IRequest<DriverAcceptTripVm>
{
    public int TripId { get; set; }
    public int DriverId { get; set; }
    public required AddressRequest DriverAddress { get; set; }
}