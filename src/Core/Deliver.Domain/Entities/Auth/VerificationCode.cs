using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deliver.Domain.Entities.Auth;

public class VerificationCode
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public required int UserId { get; set; }

    [Required]
    public required string Code { get; set; }

    [Required]
    public DateTime ExpirationDate { get; set; }

    [Required]
    public bool IsUsed { get; set; }

    [ForeignKey("UserId")]
    public virtual ApplicationUser? User { get; set; }
}