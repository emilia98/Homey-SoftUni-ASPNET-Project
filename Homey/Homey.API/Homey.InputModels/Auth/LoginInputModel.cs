using System;
using System.ComponentModel.DataAnnotations;

namespace Homey.InputModels.Auth
{
    public class LoginInputModel
    {
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
