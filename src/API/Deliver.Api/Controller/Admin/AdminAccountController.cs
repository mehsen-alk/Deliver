using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Notification.Command.DeleteNotificationTokenByDeviceId;
using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response.AdminSignIn;
using Deliver.Application.Models.Authentication.SignOut;
using Deliver.Application.Models.Authentication.SignOut.Response;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Admin;

[Route("v1/admin")]
[ApiController]
public class AdminAccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMediator _mediator;

    public AdminAccountController(
        IAuthenticationService authenticationService,
        IMediator mediator
    )
    {
        _authenticationService = authenticationService;
        _mediator = mediator;
    }

    /// <summary>
    ///     signin as a Admin.
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     {
    ///     "userName": "931464912",
    ///     "password": "123456"
    ///     }
    /// </remarks>
    /// <response code="200">logged in successfully</response>
    /// <response code="400">request is missing required parameters</response>
    /// <response code="401">wrong credential</response>
    [HttpPost("signin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
        typeof(BaseResponse<string>),
        StatusCodes.Status401Unauthorized
    )]
    public async Task<ActionResult<AdminSignInResponse>> SignInAsync(
        SignInRequest request
    )
    {
        return Ok(await _authenticationService.AdminSignInAsync(request));
    }

    [HttpPost("signOut")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SignOutResponse>> SignOutAsync(SignOutRequest request)
    {
        return Ok(
            await _mediator.Send(
                new DeleteNotificationTokenByDeviceIdCommand(request.DeviceId)
            )
        );
    }
}