using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }=string.Empty;
        public String Password { get; set; }=string.Empty;
    }
}