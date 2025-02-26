using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response.DriverSignIn;
using Deliver.Application.Models.Authentication.SignIn.Response.RiderSignIn;
using Deliver.Application.Models.Authentication.SignUp;
using Deliver.Application.Models.Authentication.SignUp.Response;

namespace Deliver.Application.Contracts.Identity;

public interface IAuthenticationService
{
    Task<RiderSignInResponse> RiderSignInAsync(SignInRequest request);
    Task<SignUpResponse> RiderSignUpAsync(SignUpRequest request);
    Task<DriverSignInResponse> DriverSignInAsync(SignInRequest request);
    Task<SignUpResponse> DriverSignUpAsync(SignUpRequest request);
    Task GenerateVerificationCodeAsync(int userId);
    Task<string> GetVerificationCodeAsync(int userId);
    Task VerifyPhoneAsync(int userId, string verificationCode);
}