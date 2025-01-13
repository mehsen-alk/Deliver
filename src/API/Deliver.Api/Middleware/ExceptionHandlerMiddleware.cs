using System.Net;
using Deliver.Application.Exceptions;
using Deliver.Application.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Deliver.Api.Middleware;

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
            SentrySdk.CaptureException(ex);
            await ConvertException(context, ex);
        }
    }

    private Task ConvertException(HttpContext context, Exception exception)
    {
        BaseResponse<object> response = new ErrorResponse<object>
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "Internal Server Error",
            Err = exception.Message,
            Data = null,
            Link = exception.HelpLink,
            InnerException = exception.InnerException?.Message,
            StackTrace = exception.StackTrace
        };

        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ValidationException validationException:
                response = new ValidationErrorResponse<object>
                {
                    ValidationErrors = validationException.ValidationErrors,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = validationException.ValidationErrors.First(),
                    Data = response.Data,
                    Err = "One or more validation errors occurred.",
                    Link = exception.HelpLink,
                    InnerException = exception.InnerException?.Message,
                    StackTrace = exception.StackTrace
                };
                break;

            case BadRequestException e:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = e.Message;
                break;

            case NotFoundException e:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Message = e.Message;
                break;

            case CredentialNotValid:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;

            case DeliverException deliverEx:
                response = new BaseResponse<object>
                {
                    StatusCode = (int)deliverEx.StatusCode,
                    Message = deliverEx.Message,
                    Data = deliverEx.Data
                };
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return ReturnException(context, response);

            case not null:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.StatusCode = response.StatusCode;

        return ReturnException(context, response);
    }

    private Task ReturnException(HttpContext context, object response)
    {
        var result = JsonConvert.SerializeObject(
            response,
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        );

        return context.Response.WriteAsync(result);
    }
}