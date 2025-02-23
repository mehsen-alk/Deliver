using System.ComponentModel.DataAnnotations.Schema;
using Deliver.Domain.common;
using Deliver.Domain.Entities.Auth;
using Deliver.Domain.Enums;

namespace Deliver.Domain.Entities;

public class Trip : AuditableEntity
{
    public int Id { get; set; }
    public TripStatus Status { get; set; }
    public int RiderId { get; set; }
    public int? DriverId { get; set; }
    public int PickUpAddressId { get; set; }
    public int DropOffAddressId { get; set; }
    public double CalculatedDistance { get; set; }
    public double CalculatedDuration { get; set; }

    [ForeignKey("RiderId")]
    public virtual ApplicationUser Rider { get; set; } = default!;

    [ForeignKey("DriverId")]
    public virtual ApplicationUser Driver { get; set; } = default!;

    [ForeignKey("PickUpAddressId")]
    public virtual Address PickUpAddress { get; set; } = default!;

    [ForeignKey("DropOffAddressId")]
    public virtual Address DropOffAddress { get; set; } = default!;
}