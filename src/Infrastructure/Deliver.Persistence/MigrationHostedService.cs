using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Persistence;

public class MigrationHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public MigrationHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DeliverDbContext>();

        if (!await CanEstablishConnectionWithDataBase(db, 100, 1000, cancellationToken))
            throw new Exception(
                $"Can't establish connection with database {db.Database.GetDbConnection().Database}."
            );

        await db.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task<bool> CanEstablishConnectionWithDataBase(
        DbContext db,
        int maxRetry,
        int millisecondsDelay,
        CancellationToken cancellationToken
    )
    {
        if (maxRetry <= 0)
        {
            Console.WriteLine(
                $"couldn't connect to DB {db.Database.GetDbConnection().Database}."
            );
            return false;
        }

        if (await db.Database.CanConnectAsync(cancellationToken)) return true;

        await Task.Delay(millisecondsDelay, cancellationToken);

        maxRetry--;
        Console.WriteLine(
            $"DB {db.Database.GetDbConnection().Database} is not ready. Retry {maxRetry} attempts."
        );
        Console.WriteLine($"DB {db.Database.GetDbConnection().ConnectionString} ");
        Console.WriteLine($"DB {db.Database.GetDbConnection().State} ");
        
        return await CanEstablishConnectionWithDataBase(
            db,
            maxRetry,
            millisecondsDelay,
            cancellationToken
        );
    }
}