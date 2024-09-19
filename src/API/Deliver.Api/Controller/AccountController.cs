﻿using Deliver.Application.Contracts.Identity;
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

        [HttpPost("clientdignup")]
        public async Task<ActionResult<SignUpResponse>> ClientSignUpAsync(SignUpRequest request)
        {
            return Ok(await _authenticationService.ClientSignUpAsync(request));
        }
    }
}