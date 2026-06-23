using Employee_Admin_Portal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Admin_Portal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var employees = _db.Employees.ToList();

            ViewBag.TotalEmployees = employees.Count;
            ViewBag.AvgSalary = employees.Count > 0
                ? Math.Round(employees.Average(e => e.Salary), 2)
                : 0;
            ViewBag.Departments = employees
                .Where(e => e.Department != null)
                .GroupBy(e => e.Department!)
                .Select(g => new { Department = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToList();
            ViewBag.RecentHires = employees
                .OrderByDescending(e => e.HiredOn)
                .Take(5)
                .ToList();
            ViewBag.TopSalaries = employees
                .OrderByDescending(e => e.Salary)
                .Take(5)
                .ToList();

            return View();
        }
    }
}
