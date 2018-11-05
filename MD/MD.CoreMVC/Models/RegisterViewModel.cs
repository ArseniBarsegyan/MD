using System.ComponentModel.DataAnnotations;
using MD.Helpers;

namespace MD.CoreMVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = ConstantsHelper.EmailIsEmpty)]
        public string Email { get; set; }

        [Required(ErrorMessage = ConstantsHelper.PasswordIsEmpty)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = ConstantsHelper.PasswordIncorrect)]
        public string ConfirmPassword { get; set; }
    }
}
