using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="UserName Required")]
        public String UserName { get; set; }=string.Empty;

        [Required(ErrorMessage ="Email Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public String Email { get; set; }=string.Empty;

        [Required(ErrorMessage ="Password Required")]
        [MinLength(10,ErrorMessage ="Require at least 10 letters")]
        [MaxLength(20,ErrorMessage ="Require Maximum 20")]
        public String Password { get; set; }=string.Empty;
    }
}