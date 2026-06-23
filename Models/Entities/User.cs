namespace Employee_Admin_Portal.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string Role { get; set; } = "Employee";
    }
}
