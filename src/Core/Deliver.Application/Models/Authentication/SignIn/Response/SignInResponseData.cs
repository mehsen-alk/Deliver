namespace Deliver.Application.Models.Authentication.SignIn.Response
{
    public class SignInResponseData
    {
        public required int Id { get; set; }
        public required bool IsPhoneNumberVerified { get; set; }
        public required string Token { get; set; }
    }
}
