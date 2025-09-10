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

    public static TripStatus? FromInt(int? value)
    {
        if (!value.HasValue)
            return null;

        return value.Value switch
        {
            0 => TripStatus.Waiting,
            1 => TripStatus.OnWayToPickupRider,
            2 => TripStatus.DriverArrivedToPickupRider,
            3 => TripStatus.Delivering,
            4 => TripStatus.Delivered,
            5 => TripStatus.Cancelled,
            _ => null
        };
    }
}