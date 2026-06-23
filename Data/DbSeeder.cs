using Employee_Admin_Portal.Models.Entities;

namespace Employee_Admin_Portal.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext db)
        {
            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new User
                    {
                        Email = "admin@portal.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        Role = "Admin"
                    },
                    new User
                    {
                        Email = "employee@portal.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Employee@123"),
                        Role = "Employee"
                    }
                );
                db.SaveChanges();
            }
        }
    }
}
