using Deliver.Domain.Enums;

namespace Deliver.Application.Features.Trips.Common.TripHistoryVm;

public class TripHistoryVm
{
    public int Id { get; set; }
    public TripStatus Status { get; set; }
    public double CalculatedDistance { get; set; }
    public double CalculatedDuration { get; set; }
    public DateTime CreatedDate { get; set; }
}