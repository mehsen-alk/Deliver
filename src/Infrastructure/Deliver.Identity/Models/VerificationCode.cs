using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deliver.Identity.Models
{
    public class VerificationCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string Code { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        [ForeignKey("Id")]
        public virtual required ApplicationUser User { get; set; }
    }
}