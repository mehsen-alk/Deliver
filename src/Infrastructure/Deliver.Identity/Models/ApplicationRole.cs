using Microsoft.AspNetCore.Identity;

namespace Deliver.Identity.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public required string Description { get; set; }
    }
}
