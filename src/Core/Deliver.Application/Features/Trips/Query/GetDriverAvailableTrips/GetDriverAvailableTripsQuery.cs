using MediatR;

namespace Deliver.Application.Features.Trips.Query.GetDriverAvailableTrips;

public class GetDriverAvailableTripsQuery : IRequest<GetDriverAvailableTripsQueryVm>
{
    public readonly int Size = 12;
    public required int Page { get; init; } = 1;
}