using System.ComponentModel.DataAnnotations.Schema;
using Deliver.Domain.common;
using Deliver.Domain.Entities.Auth;

namespace Deliver.Domain.Entities;

public class Address : AuditableEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = default!;
}