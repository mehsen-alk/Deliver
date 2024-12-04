using MediatR;

namespace Deliver.Application.Features.Trips.Query.GetDriverCurrentTrip;

public class GetDriverCurrentTripQuery : IRequest<DriverCurrentTripVm?>
{
    public int DriverId { get; init; }
}