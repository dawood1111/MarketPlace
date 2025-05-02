using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email Require")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public String Email { get; set; }=string.Empty;
        
        [Required(ErrorMessage ="Password Require")]
        public String Password { get; set; }=string.Empty;
    }
}