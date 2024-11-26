namespace Deliver.Application.Responses;

public class ErrorResponse<T> : BaseResponse<T> where T : class
{
    public required string? Err { get; set; }
}