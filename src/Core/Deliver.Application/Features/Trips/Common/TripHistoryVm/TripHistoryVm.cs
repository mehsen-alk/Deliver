using Deliver.Application.Dto.Address;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;

namespace Deliver.Application.Features.Trips.Common.TripHistoryVm;

public class TripHistoryVm
{
    public int Id { get; set; }
    public TripStatus Status { get; set; }
    public double CalculatedDistance { get; set; }
    public double CalculatedDuration { get; set; }
    public DateTime CreatedDate { get; set; }
    public required AddressDto PickUpAddress { get; init; }
    public required AddressDto DropOffAddress { get; init; }
}