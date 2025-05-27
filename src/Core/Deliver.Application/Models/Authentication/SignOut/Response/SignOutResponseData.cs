namespace Deliver.Application.Models.Authentication.SignOut.Response;

public class SignOutResponseData
{
    public required List<int> DeletedTokens { get; set; }
}