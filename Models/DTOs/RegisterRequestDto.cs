using System.ComponentModel.DataAnnotations;

namespace Employee_Admin_Portal.Models.DTOs
{
    public class RegisterRequestDto
    {
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, MinLength(6)]
        public required string Password { get; set; }

        public string Role { get; set; } = "Employee";
    }
}
