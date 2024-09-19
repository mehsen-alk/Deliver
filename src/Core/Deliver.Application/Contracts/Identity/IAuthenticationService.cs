using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response;
using Deliver.Application.Models.Authentication.SignUp;
using Deliver.Application.Models.Authentication.SignUp.response;
using Deliver.Application.Responses;
using System.Threading.Tasks;

namespace Deliver.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<SignInResponse> ClientSignInAsync(SignInRequest request);
        Task<SignUpResponse> ClientSignUpAsync(SignUpRequest request);
    }
}
