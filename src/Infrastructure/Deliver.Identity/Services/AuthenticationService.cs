using Deliver.Application.Contracts.Identity;
using Deliver.Application.Exceptions;
using Deliver.Application.Models.Authentication;
using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response;
using Deliver.Application.Models.Authentication.SignUp;
using Deliver.Application.Models.Authentication.SignUp.response;
using Deliver.Application.Models.Authentication.SignUp.Response;
using Deliver.Identity.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Deliver.Domain.Entities.Auth;

namespace Deliver.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IdentityRepository _identityRepository;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager,
            IdentityRepository identityRepository)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _identityRepository = identityRepository;
        }

        private async Task<ApplicationUser> SignInAsync(SignInRequest request, string role)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                throw new CredentialNotValid($"incorrect user name or password 56");
            }

            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new CredentialNotValid($"incorrect user name or password: 43");
            }


            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains(role))
            {
                throw new CredentialNotValid($"incorrect user name or password 55");
            }

            return user;
        }

        private async Task<ApplicationUser> SignUpAsync(SignUpRequest request, string role)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Phone);

            if (existingUser != null)
            {
                throw new CredentialNotValid($"Username '{request.Phone}' already exists.");
            }

            var user = new ApplicationUser
            {
                Name = request.Name,
                UserName = request.Phone,
                PhoneNumber = request.Phone,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {

                throw new Exception($"{result.Errors}");
            }

            await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                throw new Exception($"{result.Errors}");
            }

            return user;

        }

        public async Task<SignInResponse> RiderSignInAsync(SignInRequest request)
        {
            var user = await SignInAsync(request, "Rider");

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            SignInResponse response = new SignInResponse
            {
                StatusCode = 200,
                Message = "fetched successfully",
                Data = new SignInResponseData()
                {
                    Id = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    IsPhoneNumberVerified = user.PhoneNumberConfirmed,
                },
            };

            return response;
        }

        public async Task<SignUpResponse> RiderSignUpAsync(SignUpRequest request)
        {
            var user = await SignUpAsync(request, "Rider");

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            return new SignUpResponse()
            {
                StatusCode = 201,
                Message = "created successfully",
                Data = new SignUpResponseData()
                {
                    UserId = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                }
            };
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("userName", user.UserName ?? ""),
                new Claim("name", user.Name ?? ""),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public async Task<SignInResponse> DriverSignInAsync(SignInRequest request)
        {
            var user = await SignInAsync(request, "Driver");

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            SignInResponse response = new SignInResponse
            {
                StatusCode = 200,
                Message = "fetched successfully",
                Data = new SignInResponseData()
                {
                    Id = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    IsPhoneNumberVerified = user.PhoneNumberConfirmed,
                },
            };

            return response;
        }

        public async Task<SignUpResponse> DriverSignUpAsync(SignUpRequest request)
        {
            var user = await SignUpAsync(request, "Driver");

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            return new SignUpResponse()
            {
                StatusCode = 201,
                Message = "created successfully",
                Data = new SignUpResponseData()
                {
                    UserId = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                }
            };
        }

        public async Task GenerateVerificationCodeAsync(int userId)
        {
            var code = new Random().Next(100000, 999999).ToString();

            var expirationDate = DateTime.UtcNow.AddMinutes(10);

            var verificationCode = new VerificationCode
            {
                UserId = userId,
                Code = code,
                ExpirationDate = expirationDate,
                IsUsed = false,
            };

            await _identityRepository.AddVerificationCodeAsync(verificationCode);

            return;
        }

        public async Task<string> GetVerificationCodeAsync(int userId)
        {
            var verificationCode = await _identityRepository.GetVerificationCodeAsync(userId);

            if (verificationCode == null)
            {
                throw new NotFoundException();
            }

            return verificationCode.Code;
        }

        public async Task VerifyPhoneAsync(int userId, string code)
        {
            var verificationCode = await _identityRepository.GetVerificationCodeAsync(userId, code);

            if (verificationCode == null)
            {
                throw new NotFoundException("code not found");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NotFoundException("user not found");
            }

            user.PhoneNumberConfirmed = true;
            verificationCode.IsUsed = true;

            await _identityRepository.UpdateVerificationCodeAsync(verificationCode);

            return;
        }
    }
}
