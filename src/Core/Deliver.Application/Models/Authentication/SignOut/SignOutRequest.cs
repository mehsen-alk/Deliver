using System.ComponentModel.DataAnnotations;

namespace Deliver.Application.Models.Authentication.SignOut;

public class SignOutRequest
{
    [Required]
    [MinLength(1)]
    public required string DeviceId { get; set; }
}