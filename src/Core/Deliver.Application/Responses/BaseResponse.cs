
namespace Deliver.Application.Responses
{
    public class BaseResponse<T> where T : class?
    {
        public BaseResponse()
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

        public static BaseResponse<T> FetchedSuccessfully(int statusCode = 200, string message = "fetched successfully", T? data = null)
        {
            return new BaseResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data,
            };
        }

        public static BaseResponse<T> CreatedSuccessfully(int statusCode = 201, string message = "created successfully", T? data = null)
        {
            return new BaseResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data,
            };
        }

        public static BaseResponse<T> UpdatedSuccessfully(int statusCode = 204, string message = "updated successfully", T? data = null)
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
