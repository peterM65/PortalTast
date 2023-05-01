using System.ComponentModel.DataAnnotations;

namespace Portal.Core.Models
{
    public class ForgetPasswordVM
    {

        [Required(ErrorMessage = " Email Required")]
        [EmailAddress(ErrorMessage = "invalid mail formate")]
        public string Email { get; set; }

    }
}
