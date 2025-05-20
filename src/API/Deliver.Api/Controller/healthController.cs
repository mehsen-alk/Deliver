using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Models.Notification;
using Deliver.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller;

[Route("v1/health")]
[ApiController]
public class HealthController : ControllerBase
{
    private readonly INotificationServices _notificationServices;

    public HealthController(INotificationServices notificationServices)
    {
        _notificationServices = notificationServices;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    public ActionResult CheckServerHealth()
    {
        return Ok(
            BaseResponse<string>.FetchedSuccessfully(data: "server is up and running")
        );
    }

    [HttpGet("driver")]
    [Authorize(Roles = "Driver")]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    public ActionResult CheckServerHealthDriver()
    {
        return Ok(
            BaseResponse<string>.FetchedSuccessfully(data: "server is up and running")
        );
    }

    [HttpGet("rider")]
    [Authorize(Roles = "Rider")]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    public ActionResult CheckServerHealthRider()
    {
        return Ok(
            BaseResponse<string>.FetchedSuccessfully(data: "server is up and running")
        );
    }

    [HttpPost("sendNotificationByToken")]
    [Authorize]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    public async Task<ActionResult> CheckNotificationHealth(
        [FromBody] NotificationRequest request
    )
    {
        var notificationId = await _notificationServices.SendNotificationAsync(request);

        return Ok(BaseResponse<string>.FetchedSuccessfully(data: notificationId));
    }
}