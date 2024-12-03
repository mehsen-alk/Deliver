using System.Reflection;
using Deliver.Application.Contracts.Service;
using Deliver.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Deliver.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services
    )
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services.AddScoped<IProfitService, ProfitService>();

        return services;
    }
}