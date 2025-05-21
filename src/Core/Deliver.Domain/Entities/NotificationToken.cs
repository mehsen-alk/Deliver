using System.ComponentModel.DataAnnotations.Schema;
using Deliver.Domain.common;
using Deliver.Domain.Entities.Auth;

namespace Deliver.Domain.Entities;

public class NotificationToken : AuditableEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public string DeviceId { get; set; } = string.Empty;
    public DateTime? LastUseDate { get; set; }

    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = default!;
}