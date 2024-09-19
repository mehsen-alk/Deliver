namespace Deliver.Application.Models.Authentication.SignIn.Response
{
    public class SignInResponseData
    {
        public required int Id { get; set; }
        public required string UserName { get; set; }
        public required string Phone { get; set; }
        public required string Token { get; set; }
        public required string Role { get; set; }
    }
}
