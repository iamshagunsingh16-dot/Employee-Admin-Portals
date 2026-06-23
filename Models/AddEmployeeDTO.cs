namespace Employee_Admin_Portal.Models
{
    public class AddEmployeeDTO
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
        public string? Department { get; set; }
        public string? JobTitle { get; set; }
    }
}
