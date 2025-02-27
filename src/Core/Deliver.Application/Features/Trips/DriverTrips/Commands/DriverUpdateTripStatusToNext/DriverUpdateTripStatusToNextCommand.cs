using Deliver.Application.Features.Trips.Common.AddressRequest;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Commands.
    DriverUpdateTripStatusToNext;

public class DriverUpdateTripStatusToNextCommand : IRequest<int>
{
    public int DriverId { get; set; }
    public required AddressRequest DriverAddress { get; set; }
}