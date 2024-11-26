using System.Text;
using Deliver.Application.Contracts.Identity;
using Deliver.Application.Models.Authentication;
using Deliver.Application.Responses;
using Deliver.Domain.Entities.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Persistence.Repositories;
using Persistence.Services;

namespace Persistence;

public static class IdentityServiceExtensions
{
    public static void AddIdentityServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<DeliverDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(
            options =>
            {
                // password configuration
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            }
        );

        services.AddTransient<IAuthenticationService, AuthenticationService>();

        services
            .AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(
                o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)
                        )
                    };

                    o.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";

                            var result = JsonConvert.SerializeObject(
                                new BaseResponse<string>
                                {
                                    StatusCode = 401,
                                    Message = "401 Not authorized."
                                }
                            );

                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";

                            var result = JsonConvert.SerializeObject(
                                new BaseResponse<string>
                                {
                                    StatusCode = 403,
                                    Message = "403 Not authorized."
                                }
                            );

                            return context.Response.WriteAsync(result);
                        }
                    };
                }
            );

        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IdentityRepository>();
    }
}