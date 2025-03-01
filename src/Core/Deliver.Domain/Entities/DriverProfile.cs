using System.ComponentModel.DataAnnotations.Schema;
using Deliver.Domain.common;
using Deliver.Domain.Entities.Auth;
using Deliver.Domain.Enums;
using static System.String;

namespace Deliver.Domain.Entities;

public class DriverProfile : AuditableEntity
{
    public int Id { get; set; }
    public required int UserId { get; set; }

    public ProfileStatus Status { get; set; }

    public required string Name { get; set; } = Empty;
    public required string Phone { get; set; } = Empty;
    public string? VehicleImage { get; set; } = null;
    public string? LicenseImage { get; set; } = null;
    public string? ProfileImage { get; set; } = null;
    public int? CurrentLocationId { get; set; }

    [ForeignKey("CurrentLocationId")]
    public virtual Address CurrentLocation { get; set; } = default!;

    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = default!;
}