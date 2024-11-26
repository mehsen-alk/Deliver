using Deliver.Application.Features.Trips.Common.AddressRequest;
using MediatR;

namespace Deliver.Application.Features.Trips.CreateTrip.Commands.RiderCreateTrip;

public class RiderCreateTripCommand : IRequest<RiderCreateTripResponse>
{
    public int RiderId { get; set; }
    public AddressRequest PickUpAddress { get; set; } = default!;
    public AddressRequest DropOfAddress { get; set; } = default!;
    public double Distance { get; set; }
    public double Duration { get; set; }
}