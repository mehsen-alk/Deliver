using System.ComponentModel.DataAnnotations.Schema;
using Deliver.Domain.common;
using Deliver.Domain.Entities.Auth;
using Deliver.Domain.Enums;

namespace Deliver.Domain.Entities;

public class Payment : AuditableEntity
{
    public int Id { get; set; }
    public int? TripId { get; set; }
    public PaymentStatus Status { get; set; } = default;
    public decimal Amount { get; set; }
    public decimal CompanyCommission { get; set; }
    public PaymentType Type { get; set; } = default;
    public string Note { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; } = default;
    public string? PaymentGatewayId { get; set; } = null;
    public int FromUserId { get; set; }
    public int ToUserId { get; set; }

    [ForeignKey("FromUserId")]
    public virtual ApplicationUser FromUser { get; set; } = default!;

    [ForeignKey("ToUserId")]
    public virtual ApplicationUser ToUser { get; set; } = default!;

    [ForeignKey("TripId")]
    public virtual Trip? Trip { get; set; } = default;
}