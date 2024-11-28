using Deliver.Application.Features.Trips.Common.AddressRequest;

namespace Deliver.Application.Features.Trips.Commands.RiderCreateTrip;

public class RiderCreateTripRequest
{
    public AddressRequest PickUpAddress { get; set; } = default!;
    public AddressRequest DropOfAddress { get; set; } = default!;
    public double Distance { get; set; }
    public double Duration { get; set; }
}