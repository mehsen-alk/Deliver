using Deliver.Application.Dto.Address;
using Deliver.Domain.Enums;

namespace Deliver.Application.Features.Trips.Query.GetDriverCurrentTrip;

public class DriverCurrentTripVm
{
    public required int Id { get; init; }
    public required TripStatus Status { get; init; }
    public required AddressDto PickUpAddress { get; init; }
    public required AddressDto DropOffAddress { get; init; }
    public required DateTime CreatedDate { get; init; }
    public required double CalculatedDistance { get; init; }
    public required double CalculatedDuration { get; init; }
}