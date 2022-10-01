using System.ComponentModel.DataAnnotations;

namespace eFoodHub.UI.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Email ")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}