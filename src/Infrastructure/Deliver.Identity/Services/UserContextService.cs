using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Deliver.Identity.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var user = GetUserClaims();

            var idAsString = user?.FindFirstValue("id");

            if (idAsString == null)
            {
                throw new BadRequestException("the token dose not have the user id.");
            }

            return int.Parse(idAsString);

        }

        public string GetUserNameIdentifier()
        {
            var user = GetUserClaims();

            var userName = user?.FindFirstValue("userName");

            if (userName == null || userName.Length == 0)
            {
                throw new BadRequestException("the token dose not have the user name identifier.");
            }

            return userName;
        }

        public string GetUserName()
        {
            var user = GetUserClaims();

            var userName = user?.FindFirstValue("name");

            if (userName == null)
            {
                throw new BadRequestException("the token dose not have the user name identifier.");
            }

            return userName;
        }

        private ClaimsPrincipal GetUserClaims()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new BadRequestException("the token is in bad shape.");
            }

            return user;
        }

    }
}
