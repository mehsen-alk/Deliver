using Deliver.Application.Contracts;
using Deliver.Domain.common;
using Deliver.Domain.Entities;
using Deliver.Domain.Entities.Auth;
using Deliver.Domain.Enums;
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

    public DbSet<DriverProfile> DriversProfile { get; set; }
    public DbSet<RiderProfile> RidersProfile { get; set; }
    public DbSet<VerificationCode> VerificationCodes { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripLog> TripLogs { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<NotificationToken> NotificationTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyConfigurations(modelBuilder);

        base.OnModelCreating(modelBuilder);

        ApplySeedData(modelBuilder);
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliverDbContext).Assembly);

        ConfigureTrips(modelBuilder);
        ConfigureAddresses(modelBuilder);
        ConfigurePayments(modelBuilder);
        ConfigureNotificationTokens(modelBuilder);
    }

    private void ConfigureTrips(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Trip>()
            .HasOne(t => t.PickUpAddress)
            .WithMany()
            .HasForeignKey(t => t.PickUpAddressId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Trip>()
            .HasOne(t => t.DropOffAddress)
            .WithMany()
            .HasForeignKey(t => t.DropOffAddressId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void ConfigureAddresses(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Address>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void ConfigurePayments(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Payment>()
            .HasOne(p => p.FromUser)
            .WithMany()
            .HasForeignKey(p => p.FromUserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Payment>()
            .HasOne(p => p.ToUser)
            .WithMany()
            .HasForeignKey(p => p.ToUserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Payment>()
            .HasOne(p => p.Trip)
            .WithMany()
            .HasForeignKey(p => p.TripId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void ConfigureNotificationTokens(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationToken>().HasIndex(p => p.Token).IsUnique();

        modelBuilder
            .Entity<NotificationToken>()
            .HasIndex(p => new
                {
                    p.UserId,
                    p.DeviceId
                }
            )
            .IsUnique();
    }

    private void ApplySeedData(ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
        SeedUsers(modelBuilder);
        SeedProfiles(modelBuilder);
        SeedUserRoles(modelBuilder);
    }

    private void SeedRoles(ModelBuilder modelBuilder)
    {
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
            },
            new ApplicationRole
            {
                Id = 3,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new ApplicationRole
            {
                Id = 4,
                Name = "Super Admin",
                NormalizedName = "SUPER ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );
    }

    private void SeedUsers(ModelBuilder modelBuilder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        var user1 = new ApplicationUser
        {
            Id = 1,
            UserName = "0931464912",
            PhoneNumber = "0931464912",
            NormalizedUserName = "0931464912",
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var user2 = new ApplicationUser
        {
            Id = 2,
            UserName = "0999999999",
            PhoneNumber = "0999999999",
            NormalizedUserName = "0999999999",
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        user1.PasswordHash = hasher.HashPassword(user1, "12345678");
        user2.PasswordHash = hasher.HashPassword(user2, "12345678");

        modelBuilder.Entity<ApplicationUser>().HasData(user1, user2);
    }

    private void SeedProfiles(ModelBuilder modelBuilder)
    {
        const string defaultImageUrl =
            "https://c4d-media.s3.eu-central-1.amazonaws.com/upload/image/original-image/2023-01-29_20-32-24/scaled-image-picker8315132317025791363-63de775a3982c.jpg";

        // Rider Profiles
        modelBuilder
        .Entity<RiderProfile>()
        .HasData(
            new RiderProfile
            {
                Id = 1,
                UserId = 1,
                Name = "Mohsen Rider",
                Phone = "0931464912",
                Status = ProfileStatus.Current,
                ProfileImage = defaultImageUrl
            },
            new RiderProfile
            {
                Id = 2,
                UserId = 2,
                Name = "Assaf Rider",
                Phone = "0999999999",
                Status = ProfileStatus.Current,
                ProfileImage = defaultImageUrl
            }
        );

        // Driver Profiles
        modelBuilder
        .Entity<DriverProfile>()
        .HasData(
            new DriverProfile
            {
                Id = 1,
                UserId = 1,
                Name = "Mohsen Driver",
                Phone = "0931464912",
                Status = ProfileStatus.Current,
                ProfileImage = defaultImageUrl,
                LicenseImage = defaultImageUrl,
                VehicleImage = defaultImageUrl
            },
            new DriverProfile
            {
                Id = 2,
                UserId = 2,
                Name = "Assaf Driver",
                Phone = "0999999999",
                Status = ProfileStatus.Current,
                ProfileImage = defaultImageUrl,
                LicenseImage = defaultImageUrl,
                VehicleImage = defaultImageUrl
            }
        );
    }

    private void SeedUserRoles(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<IdentityUserRole<int>>()
            .HasData(
                // User 1 roles
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 1
                },
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 2
                },
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 3
                },
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 4
                },

                // User 2 roles
                new IdentityUserRole<int>
                {
                    UserId = 2,
                    RoleId = 1
                },
                new IdentityUserRole<int>
                {
                    UserId = 2,
                    RoleId = 2
                },
                new IdentityUserRole<int>
                {
                    UserId = 2,
                    RoleId = 3
                },
                new IdentityUserRole<int>
                {
                    UserId = 2,
                    RoleId = 4
                }
            );
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