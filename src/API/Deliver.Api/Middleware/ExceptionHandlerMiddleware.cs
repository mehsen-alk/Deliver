using Deliver.Application.Exceptions;
using Deliver.Application.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Deliver.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case CredentialNotValid:
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    break;
                case Exception ex:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;


            var result = JsonConvert.SerializeObject(
                    new BaseResponse<String>()
                    {
                        StatusCode = (int)httpStatusCode,
                        Message = exception.Message,
                        Data = exception.StackTrace,
                    },
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                );


            return context.Response.WriteAsync(result);
        }
    }
}
