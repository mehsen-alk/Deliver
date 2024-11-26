namespace Deliver.Application.Responses;

public class ValidationErrorResponse<T> : ErrorResponse<T> where T : class
{
    public required List<string> ValidationErrors { get; set; }
}