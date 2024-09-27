namespace Deliver.Application.Contracts.Identity
{
    public interface IUserContextService
    {
        int GetUserId();
        string GetUserNameIdentifier();
        string GetUserName();
    }
}
