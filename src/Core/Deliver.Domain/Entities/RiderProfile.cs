using System.ComponentModel.DataAnnotations.Schema;
using Deliver.Domain.common;
using Deliver.Domain.Entities.Auth;
using Deliver.Domain.Enums;
using static System.String;

namespace Deliver.Domain.Entities;

public class RiderProfile : AuditableEntity
{
    public int Id { get; set; }
    public required int UserId { get; set; }

    public required ProfileStatus Status { get; set; }

    public required string Name { get; set; } = Empty;
    public required string Phone { get; set; } = Empty;

    public string? ProfileImage { get; set; } = null;

    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = default!;
}