using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Contracts.Service;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Persistence.Services;

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

        // --- Initialize the Firebase Admin SDK ---
        try
        {
            FirebaseApp.Create(
                new AppOptions
                {
                    Credential = GoogleCredential.FromFile("/app/FirebaseKey.json")
                }
            );
            Console.WriteLine("Firebase Admin SDK initialized successfully!");
        }
        catch (Exception ex)
        {
            try
            {
                FirebaseApp.Create(
                    new AppOptions
                    {
                        Credential = GoogleCredential.FromFile("FirebaseKey.json")
                    }
                );
                Console.WriteLine("Firebase Admin SDK initialized successfully!");
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(
                    $"Error initializing Firebase Admin SDK: {ex.Message}"
                );
                Console.Error.WriteLine(
                    $"Error initializing Firebase Admin SDK: {exc.Message}"
                );
            }
        }
        // --- End Firebase Admin SDK initialization ---

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IRiderTripRepository, RiderTripRepository>();
        services.AddScoped<IDriverTripRepository, DriverTripRepository>();
        services.AddScoped<IDriverProfileRepository, DriverProfileRepository>();
        services.AddScoped<IRiderProfileRepository, RiderProfileRepository>();
        services.AddScoped<ITripLogRepository, TripLogRepository>();
        services.AddScoped<IDriverFinanceRepository, DriverFinanceRepository>();
        services.AddScoped<INotificationServices, FcmServices>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IRiderFinanceRepository, RiderFinanceRepository>();

        return services;
    }
}