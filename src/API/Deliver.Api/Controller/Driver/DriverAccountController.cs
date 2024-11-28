using Deliver.Application.Contracts.Identity;
using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response;
using Deliver.Application.Models.Authentication.SignUp;
using Deliver.Application.Models.Authentication.SignUp.Response;
using Deliver.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller.Driver;

[Route("v1/driver")]
[ApiController]
public class DriverAccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public DriverAccountController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
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
    public async Task<ActionResult<SignInResponse>> SignInAsync(SignInRequest request)
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
}