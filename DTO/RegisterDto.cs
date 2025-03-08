using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class RegisterDto
    {
        [Required]
        public String UserName { get; set; }=string.Empty;
          [Required]
          [EmailAddress]
        public String Email { get; set; }=string.Empty;
          [Required]
        public String Password { get; set; }=string.Empty;
    }
}