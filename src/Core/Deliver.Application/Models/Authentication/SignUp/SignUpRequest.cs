using System.ComponentModel.DataAnnotations;

namespace Deliver.Application.Models.Authentication.SignUp
{
    public class SignUpRequest
    {
        [Required]
        [MinLength(3)]
        public required string Name { get; set; }

        [Required]
        [Phone]
        public required string Phone { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }
    }
}
