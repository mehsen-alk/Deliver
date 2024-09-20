using Deliver.Application.Contracts.Identity;
using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response;
using Deliver.Application.Models.Authentication.SignUp;
using Deliver.Application.Models.Authentication.SignUp.response;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller
{
    [Route("api/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("clientsignin")]
        public async Task<ActionResult<SignInResponse>> ClientSignInAsync(SignInRequest request)
        {
            return Ok(await _authenticationService.ClientSignInAsync(request));
        }

        /// <summary>
        /// Create new Rider.
        /// </summary>
        /// <returns>the user id of the created rider</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Mohsen",
        ///        "phone": "931464912",
        ///        "password": "123456"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the Rider was created</response>
        /// <response code="400">If the request is missing required parameters</response>
        [HttpPost("clientdignup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SignUpResponse>> ClientSignUpAsync(SignUpRequest request)
        {
            return Created("", await _authenticationService.ClientSignUpAsync(request));
        }
    }
}
