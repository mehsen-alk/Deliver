namespace Deliver.Application.Models.Authentication.SignIn.Response.RiderSignIn;

public class RiderSignInResponseData
{
    public required int Id { get; set; }
    public required bool IsPhoneNumberVerified { get; set; }
    public required string Token { get; set; }
}