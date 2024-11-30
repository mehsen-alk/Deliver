namespace Deliver.Application.Exceptions;

public class DeliverException : Exception
{
    public DeliverException(
        DeliverErrorCodes statusCode,
        string? message = null,
        dynamic? data = null
    )
    {
        StatusCode = statusCode;
        Message = message ?? statusCode.GetMessage();
        Data = data;
    }

    public DeliverErrorCodes StatusCode { get; }
    public new string Message { get; }
    public new dynamic? Data { get; }
}