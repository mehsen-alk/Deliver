using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverCurrentTrip;

public class GetDriverCurrentTripQuery : IRequest<DriverCurrentTripVm?>
{
    public int DriverId { get; init; }
}