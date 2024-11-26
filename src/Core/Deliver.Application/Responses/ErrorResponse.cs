namespace Deliver.Application.Responses;

public class ErrorResponse<T> : BaseResponse<T> where T : class
{
    public required string? Err { get; set; }
    public required string? InnerException { get; set; }
    public required string? StackTrace { get; set; }
    public required string? Link { get; set; }
}