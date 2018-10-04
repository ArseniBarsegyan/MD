using System.ComponentModel.DataAnnotations;

namespace MD.RegistrationApi.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Check password minimum length", MinimumLength = 4)]
        public string Password { get; set; }
    }
}