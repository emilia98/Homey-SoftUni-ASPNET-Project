using System.ComponentModel.DataAnnotations;

namespace Homey.InputModels.Auth
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password should be at least 8 characters long!")]
        [MaxLength(20, ErrorMessage = "Password should be at most 20 charaters long!")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not in valid format!")]
        public string Email { get; set; }
    }
}
