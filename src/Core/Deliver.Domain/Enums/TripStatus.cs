namespace Deliver.Domain.Enums;

public enum TripStatus
{
    Waiting,
    OnWayToPickupClient,
    DriverArrivedToPickupClient,
    Delivering,
    Delivered,
    Cancelled
}