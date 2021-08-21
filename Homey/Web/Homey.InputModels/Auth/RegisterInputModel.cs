using System;
using System.ComponentModel.DataAnnotations;

namespace Homey.InputModels.Auth
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required]
        [Range(8, 20)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
