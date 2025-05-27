using Deliver.Application.Contracts.Identity;
using Deliver.Application.Features.Notification.Command.DeleteNotificationTokenByDeviceId;
using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response.DriverSignIn;
using Deliver.Application.Models.Authentication.SignOut;
using Deliver.Application.Models.Authentication.SignOut.Response;
using Deliver.Application.Models.Authentication.SignUp;
using Deliver.Application.Models.Authentication.SignUp.Response;
using Deliver.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Driver;

[Route("v1/driver")]
[ApiController]
public class DriverAccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMediator _mediator;

    public DriverAccountController(
        IAuthenticationService authenticationService,
        IMediator mediator
    )
    {
        _authenticationService = authenticationService;
        _mediator = mediator;
    }

    /// <summary>
    ///     signin as a Driver.
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
    public async Task<ActionResult<DriverSignInResponse>> SignInAsync(
        SignInRequest request
    )
    {
        return Ok(await _authenticationService.DriverSignInAsync(request));
    }

    /// <summary>
    ///     Create new Driver.
    /// </summary>
    /// <returns>the user id of the created Driver</returns>
    /// <remarks>
    ///     Sample request:
    ///     {
    ///     "name": "Mohsen",
    ///     "phone": "931464912",
    ///     "password": "123456"
    ///     }
    /// </remarks>
    /// <response code="201">If the Driver was created</response>
    /// <response code="400">If the request is missing required parameters</response>
    [HttpPost("signup")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<SignUpResponse>> SignUpAsync(SignUpRequest request)
    {
        return Created("", await _authenticationService.DriverSignUpAsync(request));
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