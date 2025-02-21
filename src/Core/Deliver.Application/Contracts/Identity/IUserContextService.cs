using Deliver.Domain.Entities.Auth;

namespace Deliver.Application.Contracts.Identity;

public interface IUserContextService
{
    int GetUserId();
    Task<int> GetDriverProfileId();
    Task<int> GetRiderProfileId();
    ApplicationUser GetUser();
    string GetUserNameIdentifier();
}