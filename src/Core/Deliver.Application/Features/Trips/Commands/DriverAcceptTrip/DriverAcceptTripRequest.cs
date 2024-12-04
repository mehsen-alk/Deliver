using Deliver.Application.Features.Trips.Common.AddressRequest;

namespace Deliver.Application.Features.Trips.Commands.DriverAcceptTrip;

public class DriverAcceptTripRequest
{
    public int TripId { get; set; }
    public required AddressRequest DriverAddress { get; set; }
}