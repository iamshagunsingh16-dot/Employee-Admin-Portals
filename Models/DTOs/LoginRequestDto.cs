using System.ComponentModel.DataAnnotations;

namespace Employee_Admin_Portal.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
