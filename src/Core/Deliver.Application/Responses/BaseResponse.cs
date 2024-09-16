
namespace Deliver.Application.Responses
{
    public class BaseResponse
    {
        private BaseResponse()
        {
            Message = string.Empty;
            StatusCode = 0;
        }

        public BaseResponse(int statusCode, string message)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public BaseResponse(int statusCode, string message, object data)
        {
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }

        public static BaseResponse FetchSuccessfully(int statusCode = 200, string message = "fetched successfully", object? data = null)
        {
            return new BaseResponse
            {
                StatusCode = statusCode,
                Message = message,
                Data = data,
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
    }
}
