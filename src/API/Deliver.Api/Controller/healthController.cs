using Deliver.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller;

[Route("v1/health")]
[ApiController]
public class HealthController : ControllerBase
{
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
    
    [HttpGet("ci/cd/Worked")]
    [Authorize(Roles = "Driver")]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    public ActionResult CheckServerHealthDriver3()
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
}