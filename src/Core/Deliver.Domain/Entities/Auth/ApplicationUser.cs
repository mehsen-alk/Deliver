using Microsoft.AspNetCore.Identity;

namespace Deliver.Domain.Entities.Auth
{
    public class ApplicationUser : IdentityUser<int>
    {
        public required string Name { get; set; }
    }
}
