namespace Deliver.Application.Models.Authentication.SignUp.response
{
    public class SignUpResponseData
    {
        public required int UserId { get; set; }

        public required string Token { get; set; }
    }
}
