using System.ComponentModel.DataAnnotations;

namespace Deliver.Application.Models.Authentication.Account;

public class VerifyPhoneRequest
{
    [Required(ErrorMessage = "code is required")]
    [MinLength(6)]
    [MaxLength(6)]
    public required string Code { get; set; }
}