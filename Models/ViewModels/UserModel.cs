using System.ComponentModel.DataAnnotations;

namespace AspDotNetCoreApi6.Models.ViewModels
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters allowed!")]
        public string? FirstName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters allowed!")]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Please enter a valid email!")]
        public string? Email { get; set; }
        public string? Address { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Phone number should be of 10 digits", MinimumLength = 10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
        public string? Mobile { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Password should be minimum of 8 characters including one lower case letter, one upper case letter, special character and one number", MinimumLength = 8)]
        [RegularExpression("^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$")]
        public string? Password { get; set; }
    }
}
