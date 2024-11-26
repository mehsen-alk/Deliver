using System.ComponentModel.DataAnnotations;

namespace Deliver.Application.Models.Authentication.SignIn;

public class SignInRequest
{
    [Required(ErrorMessage = "userName is required")]
    [Phone]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "password is required")]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}