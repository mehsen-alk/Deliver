using System.Security.Claims;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Exceptions;
using Deliver.Domain.Entities.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserContextService(
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public int GetUserId()
    {
        var user = GetUserClaims();

        var idAsString = user?.FindFirstValue("id");

        if (idAsString == null)
            throw new BadRequestException("the token dose not have the user id.");

        return int.Parse(idAsString);
    }

    public string GetUserNameIdentifier()
    {
        var user = GetUserClaims();

        var userName = user?.FindFirstValue("userName");

        if (string.IsNullOrEmpty(userName))
            throw new BadRequestException(
                "the token dose not have the user name identifier."
            );

        return userName;
    }

    public ApplicationUser GetUser()
    {
        var userId = GetUserId();

        var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);

        if (user == null)
            throw new BadRequestException("user not found.");

        return user;
    }

    private ClaimsPrincipal GetUserClaims()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
            throw new BadRequestException("the token is in bad shape.");

        return user;
    }
}