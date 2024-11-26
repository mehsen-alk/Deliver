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