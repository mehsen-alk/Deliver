using System.ComponentModel.DataAnnotations.Schema;
using Deliver.Domain.common;
using Deliver.Domain.Enums;

namespace Deliver.Domain.Entities;

public class TripLog : AuditableEntity
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public int? DriverLocationId { get; set; }
    public TripStatus Status { get; set; }
    public TripLogType Type { get; set; }
    public string? Note { get; set; }

    [ForeignKey("TripId")]
    public virtual Trip Trip { get; set; } = default!;

    [ForeignKey("DriverLocationId")]
    public virtual Address DriverLocation { get; set; } = default!;
}