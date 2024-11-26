using Deliver.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<DeliverDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString("DeliverConnectionString"),
                b => b.MigrationsAssembly(typeof(DeliverDbContext).Assembly.FullName)
            )
        );

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IRiderTripRepository, RiderTripRepository>();

        return services;
    }
}