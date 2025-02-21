using Deliver.Domain.Entities.Auth;

namespace Deliver.Application.Contracts.Identity;

public interface IUserContextService
{
    int GetUserId();
    Task<int> GetDriverProfileId();
    ApplicationUser GetUser();
    string GetUserNameIdentifier();
}