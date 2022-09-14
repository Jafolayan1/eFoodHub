using System.ComponentModel.DataAnnotations;

namespace eFoodHub.UI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}