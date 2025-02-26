namespace Deliver.Application.Models.Authentication.SignIn.Response.DriverSignIn;

public class DriverSignInResponseData
{
    public required int Id { get; set; }
    public required bool IsPhoneNumberVerified { get; set; }
    public required bool IsVehicleRegistered { get; set; }
    public required string Token { get; set; }
}