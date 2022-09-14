using System.ComponentModel.DataAnnotations;

namespace eFoodHub.UI.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Email ")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Enter FullName ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match ")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number ")]
        public string PhoneNumber { get; set; }
    }
}