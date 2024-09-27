using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response;
using Deliver.Application.Models.Authentication.SignUp;
using Deliver.Application.Models.Authentication.SignUp.Response;
using Deliver.Application.Responses;
using System.Threading.Tasks;

namespace Deliver.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<SignInResponse> RiderSignInAsync(SignInRequest request);
        Task<SignUpResponse> RiderSignUpAsync(SignUpRequest request);
        Task<SignInResponse> DriverSignInAsync(SignInRequest request);
        Task<SignUpResponse> DriverSignUpAsync(SignUpRequest request);
        Task<BaseResponse<string>> GenerateActivationCodeAsync();
    }
}
