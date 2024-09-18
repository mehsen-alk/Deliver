using Deliver.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Deliver.Identity
{
    public class DeliverIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DeliverIdentityDbContext(DbContextOptions<DeliverIdentityDbContext> options) : base(options)
        {
        }
    }
}
