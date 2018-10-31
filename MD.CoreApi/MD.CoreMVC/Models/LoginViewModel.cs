using System.ComponentModel.DataAnnotations;
using MD.Helpers;

namespace MD.CoreMVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password))]
        public string Password { get; set; }

        [Display(Name = ConstantsHelper.RememberMe)]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
