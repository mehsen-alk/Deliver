using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Exceptions;
using Deliver.Application.Models.Authentication;
using Deliver.Application.Models.Authentication.SignIn;
using Deliver.Application.Models.Authentication.SignIn.Response.DriverSignIn;
using Deliver.Application.Models.Authentication.SignIn.Response.RiderSignIn;
using Deliver.Application.Models.Authentication.SignUp;
using Deliver.Application.Models.Authentication.SignUp.Response;
using Deliver.Domain.Entities;
using Deliver.Domain.Entities.Auth;
using Deliver.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repositories;

namespace Persistence.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAsyncRepository<RiderProfile> _clientProfileRepository;
    private readonly IDriverProfileRepository _driverProfileRepository;
    private readonly IdentityRepository _identityRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        IdentityRepository identityRepository,
        IAsyncRepository<RiderProfile> clientProfileRepository,
        IDriverProfileRepository driverProfileRepository
    )
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _identityRepository = identityRepository;
        _clientProfileRepository = clientProfileRepository;
        _driverProfileRepository = driverProfileRepository;
    }

    public async Task<RiderSignInResponse> RiderSignInAsync(SignInRequest request)
    {
        var user = await SignInAsync(request, "Rider");

        var jwtSecurityToken = await GenerateToken(user);

        var response = new RiderSignInResponse
        {
            StatusCode = 200,
            Message = "fetched successfully",
            Data = new RiderSignInResponseData
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                IsPhoneNumberVerified = user.PhoneNumberConfirmed
            }
        };

        return response;
    }

    public async Task<DriverSignInResponse> DriverSignInAsync(SignInRequest request)
    {
        var user = await SignInAsync(request, "Driver");

        var jwtSecurityToken = await GenerateToken(user);

        var driverProfile =
            await _driverProfileRepository.GetDriverCurrentProfile(user.Id);

        var isVehicleRegistered = driverProfile is
            { LicenseImage: { Length: > 0 }, VehicleImage.Length: > 0 };

        var response = new DriverSignInResponse
        {
            StatusCode = 200,
            Message = "fetched successfully",
            Data = new DriverSignInResponseData
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                IsPhoneNumberVerified = user.PhoneNumberConfirmed,
                IsVehicleRegistered = isVehicleRegistered
            }
        };

        return response;
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
            IsUsed = false
        };

        await _identityRepository.AddVerificationCodeAsync(verificationCode);
    }

    public async Task<string> GetVerificationCodeAsync(int userId)
    {
        var verificationCode = await _identityRepository.GetVerificationCodeAsync(userId);

        if (verificationCode == null) throw new NotFoundException();

        return verificationCode.Code;
    }

    public async Task VerifyPhoneAsync(int userId, string code)
    {
        var verificationCode = await _identityRepository.GetVerificationCodeAsync(
            userId,
            code
        );

        if (verificationCode == null)
            throw new NotFoundException("code not found");

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new NotFoundException("user not found");

        user.PhoneNumberConfirmed = true;
        verificationCode.IsUsed = true;

        await _identityRepository.UpdateVerificationCodeAsync(verificationCode);
    }

    public async Task<SignUpResponse> RiderSignUpAsync(SignUpRequest request)
    {
        var user = await SignUpAsync(request, "Rider");

        var jwtSecurityToken = await GenerateToken(user);

        var profile = new RiderProfile
        {
            UserId = user.Id,
            Name = request.Name,
            Status = ProfileStatus.Current,
            Phone = request.Phone
        };

        await _clientProfileRepository.AddAsync(profile);

        return new SignUpResponse
        {
            StatusCode = 201,
            Message = "created successfully",
            Data = new SignUpResponseData
            {
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            }
        };
    }

    public async Task<SignUpResponse> DriverSignUpAsync(SignUpRequest request)
    {
        var user = await SignUpAsync(request, "Driver");

        var jwtSecurityToken = await GenerateToken(user);

        var profile = new DriverProfile
        {
            UserId = user.Id,
            Name = request.Name,
            Status = ProfileStatus.Current,
            Phone = request.Phone
        };

        await _driverProfileRepository.AddAsync(profile);

        return new SignUpResponse
        {
            StatusCode = 201,
            Message = "created successfully",
            Data = new SignUpResponseData
            {
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            }
        };
    }

    private async Task<ApplicationUser> SignInAsync(SignInRequest request, string role)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
            throw new CredentialNotValid("incorrect user name or password 56");

        var result = await _signInManager.PasswordSignInAsync(
            request.UserName,
            request.Password,
            false,
            false
        );

        if (!result.Succeeded)
            throw new CredentialNotValid("incorrect user name or password: 43");

        var roles = await _userManager.GetRolesAsync(user);

        if (!roles.Contains(role))
            throw new CredentialNotValid("incorrect user name or password 55");

        return user;
    }

    private async Task<ApplicationUser> SignUpAsync(SignUpRequest request, string role)
    {
        var existingUser = await _userManager.FindByNameAsync(request.Phone);

        if (existingUser != null)
            throw new CredentialNotValid($"Username '{request.Phone}' already exists.");

        var user = new ApplicationUser
        {
            UserName = request.Phone,
            PhoneNumber = request.Phone
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new Exception($"{result.Errors}");

        await _userManager.AddToRoleAsync(user, role);

        if (!result.Succeeded)
            throw new Exception($"{result.Errors}");

        return user;
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (var i = 0; i < roles.Count; i++)
            roleClaims.Add(new Claim("roles", roles[i]));

        var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("userName", user.UserName ?? "")
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key)
        );
        var signingCredentials = new SigningCredentials(
            symmetricSecurityKey,
            SecurityAlgorithms.HmacSha256
        );

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials
        );
        return jwtSecurityToken;
    }
}