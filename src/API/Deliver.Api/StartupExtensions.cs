using System.Reflection;
using Deliver.Api.Converters;
using Deliver.Api.Middleware;
using Deliver.Api.Service;
using Deliver.Application;
using Deliver.Application.Contracts;
using Deliver.Application.Features.Profiles.DriverProfile.Commands.EditProfileByDriver;
using Deliver.Application.Validations;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Deliver.Api;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options => { options.ListenAnyIP(8080); });

        AddSwagger(builder.Services);

        builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddApplicationServices();

        builder
            .Services.AddControllers()
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new DateTimeToTimestampConverter()
                    );
                }
            )
            .ConfigureApiBehaviorOptions(
                options => { options.SuppressModelStateInvalidFilter = true; }
            );

        builder.Services.AddIdentityServices(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddHostedService<MigrationHostedService>();

        builder.Services.AddCors(
            options =>
            {
                options.AddPolicy(
                    "Open",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    }
                );
            }
        );

        // add all validators from domain layer, note that  EditProfileByDriverCommandValidator located in the domain layer so AddValidatorsFromAssemblyContaining will search in the domain layer
        builder.Services
            .AddValidatorsFromAssemblyContaining<EditProfileByDriverCommandValidator>();
        builder.Services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );

        builder.WebHost.UseSentry(
            o =>
            {
                o.Dsn =
                    "https://e7cb076d9b92c3d52a47a195720c82d6@o4508568254939136.ingest.de.sentry.io/4508568257888337";
                o.Debug = true;
                o.TracesSampleRate = 1.0;
            }
        );

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("../swagger/v1/swagger.json", "Deliver API");
                    options.RoutePrefix = string.Empty;
                }
            );
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseCustomExceptionHandler();

        app.UseCors("Open");

        app.MapControllers();

        return app;
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            c =>
            {
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description =
                            @"JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    }
                );

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                        }
                    }
                );

                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Deliver API"
                    }
                );

                c.OperationFilter<FileResultContentTypeOperationFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }
        );
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder =>
                {
                    // Add the following line:
                    webBuilder.UseSentry(
                        o =>
                        {
                            o.Dsn =
                                "https://e7cb076d9b92c3d52a47a195720c82d6@o4508568254939136.ingest.de.sentry.io/4508568257888337";
                            // When configuring for the first time, to see what the SDK is doing:
                            o.Debug = true;
                            // Set TracesSampleRate to 1.0 to capture 100%
                            // of transactions for tracing.
                            // We recommend adjusting this value in production
                            o.TracesSampleRate = 1.0;
                        }
                    );
                }
            );
    }

    /// <summary>
    ///     Indicates swashbuckle should expose the result of the method as a file in open api
    ///     (see
    ///     https://swagger.io/docs/specification/describing-responses/)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    private class FileResultContentTypeAttribute : Attribute
    {
        public FileResultContentTypeAttribute(string contentType)
        {
            ContentType = contentType;
        }

        /// <summary>
        ///     Content type of the file e.g. image/png
        /// </summary>
        public string ContentType { get; }
    }

    private class FileResultContentTypeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requestAttribute = context
                .MethodInfo.GetCustomAttributes(
                    typeof(FileResultContentTypeAttribute),
                    false
                )
                .Cast<FileResultContentTypeAttribute>()
                .FirstOrDefault();

            if (requestAttribute == null) return;

            operation.Responses.Clear();
            operation.Responses.Add(
                "200",
                new OpenApiResponse
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        {
                            requestAttribute.ContentType,
                            new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                }
                            }
                        }
                    }
                }
            );
        }
    }
}