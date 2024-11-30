namespace Deliver.Application.Exceptions;

public enum DeliverErrorCodes
{
    ActiveTripExists = 1001
}

public static class DeliverErrorCodesExtensions
{
    public static string GetMessage(this DeliverErrorCodes code)
    {
        switch (code)
        {
            case DeliverErrorCodes.ActiveTripExists:
                return "Active Trip Exists";

            default:
                return "Unknown Error";
        }
    }
}