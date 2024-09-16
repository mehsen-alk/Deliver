using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Deliver.Api
{

    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            AddSwagger(builder.Services);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();

            builder.Services.AddCors(
                options =>
                {
                    options.AddPolicy("Open", builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
                }
            );

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "GloboTicket Ticket Management API");
                });
            }

            app.UseHttpsRedirection();

            app.UseCors("Open");

            app.MapControllers();

            return app;
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
              {
                  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                  {
                      Description = @"JWT Authorization header using the Bearer scheme.",
                      Name = "Authorization",
                      In = ParameterLocation.Header,
                      Type = SecuritySchemeType.ApiKey,
                      Scheme = "Bearer"
                  });

                  c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                      });

                  c.SwaggerDoc("v1", new OpenApiInfo
                  {
                      Version = "v1",
                      Title = "GloboTicket Ticket Management API",

                  });

                  c.OperationFilter<FileResultContentTypeOperationFilter>();
              });
        }


        /// <summary>
        /// Indicates swashbuckle should expose the result of the method as a file in open api (see https://swagger.io/docs/specification/describing-responses/)
        /// </summary>
        [AttributeUsage(AttributeTargets.Method)]
        public class FileResultContentTypeAttribute : Attribute
        {
            public FileResultContentTypeAttribute(string contentType)
            {
                ContentType = contentType;
            }

            /// <summary>
            /// Content type of the file e.g. image/png
            /// </summary>
            public string ContentType { get; }
        }

        public class FileResultContentTypeOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var requestAttribute = context.MethodInfo.GetCustomAttributes(typeof(FileResultContentTypeAttribute), false)
                    .Cast<FileResultContentTypeAttribute>()
                    .FirstOrDefault();

                if (requestAttribute == null) return;

                operation.Responses.Clear();
                operation.Responses.Add("200", new OpenApiResponse
                {
                    Content = new Dictionary<string, OpenApiMediaType>
            {
                {
                    requestAttribute.ContentType, new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "string",
                            Format = "binary"
                        }
                    }
                }
            }
                });
            }
        }

    }
}