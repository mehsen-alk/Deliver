using Deliver.Application.Features.Trips.Common.AddressRequest;

namespace Deliver.Application.Features.Trips.CreateTrip.Commands.ClientCreateTrip;

public class ClientCreateTripRequest
{
    public AddressRequest PickUpAddress { get; set; } = default!;
    public AddressRequest DropOfAddress { get; set; } = default!;
    public double Distance { get; set; }
    public double Duration { get; set; }
}