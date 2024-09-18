
namespace Deliver.Application.Responses
{
    public class BaseResponse<T> where T : class?
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

        public BaseResponse(int statusCode, string message, T data)
        {
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }

        public static BaseResponse<T> FetchSuccessfully(T? data, int statusCode = 200, string message = "fetched successfully")
        {
            return new BaseResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data,
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
