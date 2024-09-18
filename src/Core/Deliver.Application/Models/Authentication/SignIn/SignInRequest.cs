namespace Deliver.Application.Models.Authentication.SignIn
{
    public class SignInRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
