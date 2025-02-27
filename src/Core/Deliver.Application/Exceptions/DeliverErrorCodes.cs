#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
namespace Deliver.Application.Exceptions;

public enum DeliverErrorCodes
{
    ActiveTripExists = 1001,
    TripAcceptedByAnotherDriver = 1002,
    TripAlreadyAccepted = 1003,
    CannotAcceptBecauseTripStatusIsNotValid = 1004,
    YouDontHaveAnActiveTrip = 1005,
    YouHaveExceededTheTimeAllowedToCancelTrip = 1006,
    TripStatusCantBeUpdatedToNextStatus = 1007
}

public static class DeliverErrorCodesExtensions
{
    public static string GetMessage(this DeliverErrorCodes code)
    {
        return code switch
        {
            DeliverErrorCodes.ActiveTripExists => "Active Trip Exists",

            DeliverErrorCodes.CannotAcceptBecauseTripStatusIsNotValid =>
                "Cannot Accept Because Trip Status is not valid",
            DeliverErrorCodes.TripAcceptedByAnotherDriver =>
                "Trip Accepted By Another Driver",
            DeliverErrorCodes.TripAlreadyAccepted => "Trip Already Accepted",
            DeliverErrorCodes.YouDontHaveAnActiveTrip => "You Don't Have An Active Trip",
            DeliverErrorCodes.YouHaveExceededTheTimeAllowedToCancelTrip =>
                "You have exceeded the time allowed to cancel the trip.",
            DeliverErrorCodes.TripStatusCantBeUpdatedToNextStatus =>
                "Trip Status Cant Be Updated To Next Status"
        };
    }
}