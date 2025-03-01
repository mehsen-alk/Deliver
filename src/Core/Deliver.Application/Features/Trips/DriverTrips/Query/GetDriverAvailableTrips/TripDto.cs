using Deliver.Application.Dto.Address;

namespace Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverAvailableTrips;

public class TripDto
{
    public int Id { get; init; }
    public AddressDto? PickUpAddress { get; init; }
    public AddressDto? DropOffAddress { get; init; }
    public double? CalculatedDistance { get; init; }
    public double? CalculatedDuration { get; init; }
    public double? CaptainProfit { get; set; }
    public DateTime CreatedDate { get; init; }
}