using Deliver.Domain.Entities.Auth;

namespace Deliver.Application.Contracts.Identity;

public interface IUserContextService
{
    int GetUserId();
    ApplicationUser GetUser();
    string GetUserNameIdentifier();
    string GetUserName();
}