namespace Deliver.Application.Models.Authentication.SignUp.Response;

public class SignUpResponseData
{
    public required int UserId { get; set; }

    public required string Token { get; set; }
}