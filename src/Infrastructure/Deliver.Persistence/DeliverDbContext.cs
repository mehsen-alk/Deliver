using Deliver.Application.Contracts;
using Deliver.Domain.common;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DeliverDbContext : DbContext
{
    private readonly ILoggedInUserService? _loggedInUserService;

    public DeliverDbContext(DbContextOptions<DeliverDbContext> options) : base(options)
    {
    }

    public DeliverDbContext(DbContextOptions<DeliverDbContext> options, ILoggedInUserService loggedInUserService) : base(options)
    {
        _loggedInUserService = loggedInUserService;
    }

    // public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliverDbContext).Assembly);

        //seed data, added through migrations

        // modelBuilder.Entity<Category>().HasData(new Category
        // {
        //     CategoryId = concertGuid,
        //     Name = "Concerts"
        // });
        
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State) 
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = _loggedInUserService?.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = _loggedInUserService?.UserId;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

