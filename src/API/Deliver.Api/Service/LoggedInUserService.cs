using System.Security.Claims;
using Deliver.Application.Contracts;

namespace Deliver.Api.Service;

public class LoggedInUserService : ILoggedInUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
    }

    public int? UserId => int.TryParse(
        _contextAccessor.HttpContext?.User?.FindFirstValue("id"),
        out var userId
    )
        ? userId
        : null;
}