﻿using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response;
using Deliver.Application.Models.Authentication.SignUp;
using Deliver.Application.Models.Authentication.SignUp.Response;

namespace Deliver.Application.Contracts.Identity;

public interface IAuthenticationService
{
    Task<SignInResponse> RiderSignInAsync(SignInRequest request);
    Task<SignUpResponse> RiderSignUpAsync(SignUpRequest request);
    Task<SignInResponse> DriverSignInAsync(SignInRequest request);
    Task<SignUpResponse> DriverSignUpAsync(SignUpRequest request);
    Task GenerateVerificationCodeAsync(int userId);
    Task<string> GetVerificationCodeAsync(int userId);
    Task VerifyPhoneAsync(int userId, string verificationCode);
}