using System.ComponentModel.DataAnnotations.Schema;
using Deliver.Domain.common;
using Deliver.Domain.Entities.Auth;
using Deliver.Domain.Enums;

namespace Deliver.Domain.Entities;

public class Trip : AuditableEntity
{
    public int Id { get; set; }
    public TripStatus Status { get; set; }
    public int ClientId { get; set; }
    public int? DriverId { get; set; }
    public int PickUpAddressId { get; set; }
    public int DropOfAddressId { get; set; }
    public double CalculatedDistance { get; set; }
    public double CalculatedDuration { get; set; }

    [ForeignKey("ClientId")]
    public virtual ApplicationUser Client { get; set; } = default!;

    [ForeignKey("DriverId")]
    public virtual ApplicationUser Driver { get; set; } = default!;

    [ForeignKey("PickUpAddressId")]
    public virtual Address PickUpAddress { get; set; } = default!;

    [ForeignKey("DropOfAddressId")]
    public virtual Address DropOfAddress { get; set; } = default!;
}