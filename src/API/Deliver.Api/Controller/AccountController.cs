using Deliver.Application.Contracts.Identity;
using Deliver.Application.Models.Authentication.Account;
using Deliver.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliver.Api.Controller
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserContextService _userContextService;

        public AccountController(
            IAuthenticationService authenticationService,
            IUserContextService userContextService
        )
        {
            _authenticationService = authenticationService;
            _userContextService = userContextService;
        }

        [HttpPost("generateVerificationCode")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<BaseResponse<string>>> GenerateVerificationToken()
        {
            var userId = _userContextService.GetUserId();

            await _authenticationService.GenerateVerificationCodeAsync(userId);

            return Created("", BaseResponse<string>.CreatedSuccessfully());
        }

        [HttpGet("getVerificationCode")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<string>>> GrtVerificationToken()
        {
            var userId = _userContextService.GetUserId();

            var verificationCode = await _authenticationService.GetVerificationCodeAsync(userId);

            return Ok(BaseResponse<string>.FetchedSuccessfully(data: verificationCode));
        }

        [HttpPut("verifyPhone")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<ActionResult<BaseResponse<string>>> GrtVerificationToken(VerifyPhoneRequest verifyPhoneRequest)
        {
            var userId = _userContextService.GetUserId();

            await _authenticationService.VerifyPhoneAsync(userId, verifyPhoneRequest.Code);

            return StatusCode(StatusCodes.Status202Accepted, BaseResponse<string>.UpdatedSuccessfully());
        }

    }
}
