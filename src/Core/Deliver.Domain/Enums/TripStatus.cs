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
}