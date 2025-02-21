using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.DriverProfile.Common;
using Deliver.Application.Features.DriverProfile.Query.GetDriverProfileForDriver;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Driver;

[Route("v1/driver/profile")]
[ApiController]
public class DriverProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContextService;

    public DriverProfileController(
        IMediator mediator,
        IUserContextService userContextService
    )
    {
        _mediator = mediator;
        _userContextService = userContextService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(BaseResponse<string>),
        StatusCodes.Status401Unauthorized
    )]
    public async Task<ActionResult<DriverProfileVm>> GetProfile()
    {
        var userId = _userContextService.GetUserId();
        var query = new GetDriverProfileForDriverQuery { UserId = userId };

        var profile = await _mediator.Send(query);

        return Ok(profile);
    }
}