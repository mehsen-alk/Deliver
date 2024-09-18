using Microsoft.AspNetCore.Identity;

namespace Deliver.Identity.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public required string Name { get; set;}
    }
}
