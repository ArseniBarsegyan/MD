using System.ComponentModel.DataAnnotations;

namespace MD.CoreApi.Dto
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}