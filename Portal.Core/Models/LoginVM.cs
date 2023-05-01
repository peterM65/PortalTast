﻿using System.ComponentModel.DataAnnotations;

namespace Portal.Core.Models
{
    public class LoginVM
    {

        [Required(ErrorMessage = " Email Required")]
        [EmailAddress(ErrorMessage = "invalid mail formate")]
        public string Email { get; set; }


        [Required(ErrorMessage = " Password Required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        [MaxLength(8, ErrorMessage = "Max Len 8")]
        public string Password { get; set; }


        public bool RememberMe { get; set; }

    }
}
