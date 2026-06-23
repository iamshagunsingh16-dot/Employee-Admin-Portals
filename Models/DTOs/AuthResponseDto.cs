namespace Employee_Admin_Portal.Models.DTOs
{
    public class AuthResponseDto
    {
        public required string Token { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public DateTime Expires { get; set; }
    }
}
