using Deliver.Application.Contracts;
using Deliver.Domain.common;
using Deliver.Domain.Entities;
using Deliver.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DeliverDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    private readonly ILoggedInUserService? _loggedInUserService;

    public DeliverDbContext(DbContextOptions<DeliverDbContext> options) : base(options)
    {
    }

    public DeliverDbContext(
        DbContextOptions<DeliverDbContext> options,
        ILoggedInUserService loggedInUserService
    ) : base(options)
    {
        _loggedInUserService = loggedInUserService;
    }

    public DbSet<VerificationCode> VerificationCodes { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripLog> TripLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliverDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        //seed data, added through migrations

        // seed roles
        modelBuilder
        .Entity<ApplicationRole>()
        .HasData(
            new ApplicationRole
            {
                Id = 1,
                Name = "Rider",
                NormalizedName = "RIDER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new ApplicationRole
            {
                Id = 2,
                Name = "Driver",
                NormalizedName = "DRIVER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );

        // seed users
        // a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<ApplicationUser>();

        var user1 = new ApplicationUser
        {
            Id = 1,
            Name = "Mohsen",
            UserName = "221234",
            PhoneNumber = "221234",
            NormalizedUserName = "221234",
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var user2 = new ApplicationUser
        {
            Id = 2,
            Name = "Mohammed",
            UserName = "331234",
            PhoneNumber = "331234",
            NormalizedUserName = "331234",
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        user1.PasswordHash = hasher.HashPassword(user1, "123456");
        user2.PasswordHash = hasher.HashPassword(user2, "123456");

        modelBuilder.Entity<ApplicationUser>().HasData(user1, user2);

        // seed users role
        modelBuilder
            .Entity<IdentityUserRole<int>>()
            .HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                },
                new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 2
                }
            );

        modelBuilder
            .Entity<Trip>()
            .HasOne(t => t.PickUpAddress)
            .WithMany()
            .HasForeignKey(t => t.PickUpAddressId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Trip>()
            .HasOne(t => t.PickUpAddress)
            .WithMany()
            .HasForeignKey(t => t.PickUpAddressId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Address>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = new()
    )
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = _loggedInUserService?.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = _loggedInUserService?.UserId;
                    break;
            }

        return base.SaveChangesAsync(cancellationToken);
    }
}