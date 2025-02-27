#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
namespace Deliver.Domain.Enums;

public enum TripStatus
{
    Waiting,
    OnWayToPickupRider,
    DriverArrivedToPickupRider,
    Delivering,
    Delivered,
    Cancelled
}

public static class TripStatusExtensions
{
    private static readonly HashSet<TripStatus> ActiveStatuses =
    [
        TripStatus.Waiting,
        TripStatus.OnWayToPickupRider,
        TripStatus.DriverArrivedToPickupRider,
        TripStatus.Delivering
    ];

    public static HashSet<TripStatus> GetActiveStatuses()
    {
        return ActiveStatuses;
    }

    public static bool IsActive(this TripStatus tripStatus)
    {
        return ActiveStatuses.Contains(tripStatus);
    }

    public static TripStatus NextStatus(this TripStatus tripStatus)
    {
        return tripStatus switch
        {
            TripStatus.Waiting => TripStatus.OnWayToPickupRider,
            TripStatus.OnWayToPickupRider => TripStatus.DriverArrivedToPickupRider,
            TripStatus.DriverArrivedToPickupRider => TripStatus.Delivering,
            TripStatus.Delivering => TripStatus.Delivered,
            TripStatus.Delivered => throw new ArgumentOutOfRangeException(
                nameof(tripStatus),
                tripStatus,
                null
            ),
            TripStatus.Cancelled => throw new ArgumentOutOfRangeException(
                nameof(tripStatus),
                tripStatus,
                null
            )
        };
    }
}