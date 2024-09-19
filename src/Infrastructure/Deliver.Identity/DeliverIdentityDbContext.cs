using Deliver.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Deliver.Identity
{
    public class DeliverIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DeliverIdentityDbContext(DbContextOptions<DeliverIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

            // seed roles
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole()
                {
                    Id = 1,
                    Name = "Rider",
                    NormalizedName = "RIDER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ApplicationRole()
                {
                    Id = 2,
                    Name = "Driver",
                    NormalizedName = "DRIVER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
            );


            // seed users
            // a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();

            var user1 = new ApplicationUser()
            {
                Id = 1,
                UserName = "221234",
                NormalizedUserName = "221234",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var user2 = new ApplicationUser()
            {
                Id = 2,
                UserName = "331234",
                NormalizedUserName = "331234",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user1.PasswordHash = hasher.HashPassword(user1, "123456");
            user2.PasswordHash = hasher.HashPassword(user2, "123456");

            modelBuilder.Entity<ApplicationUser>().HasData(
                user1,
                user2
            );

            // seed users role
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 },
                new IdentityUserRole<int> { RoleId = 2, UserId = 2 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}