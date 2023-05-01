﻿using System.ComponentModel.DataAnnotations;

namespace Portal.Core.Models
{
    public class ResetPassword
    {

        [Required(ErrorMessage = " Password Required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        [MaxLength(8, ErrorMessage = "Max Len 8")]
        public string Password { get; set; }

        [Required(ErrorMessage = " Confirm Password Required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        [MaxLength(8, ErrorMessage = "Max Len 8")]
        [Compare("Password", ErrorMessage = "password not match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }

    }
}
