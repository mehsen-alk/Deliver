using Deliver.Application.Features.Trips.Common.AddressRequest;
using MediatR;

namespace Deliver.Application.Features.Trips.Commands.RiderCreateTrip;

public class RiderCreateTripCommand : IRequest<RiderCreateTripDto>
{
    public int RiderId { get; set; }
    public AddressRequest PickUpAddress { get; set; } = default!;
    public AddressRequest DropOffAddress { get; set; } = default!;
    public double Distance { get; set; }
    public double Duration { get; set; }
}