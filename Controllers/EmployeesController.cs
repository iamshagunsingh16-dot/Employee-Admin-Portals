using Employee_Admin_Portal.Data;
using Employee_Admin_Portal.Models;
using Employee_Admin_Portal.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Admin_Portal.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string? search, string? department)
        {
            var query = _db.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(e =>
                    e.Name.Contains(search) ||
                    e.Email.Contains(search) ||
                    (e.JobTitle != null && e.JobTitle.Contains(search)));

            if (!string.IsNullOrWhiteSpace(department))
                query = query.Where(e => e.Department == department);

            ViewBag.Search = search;
            ViewBag.Department = department;
            ViewBag.Departments = _db.Employees
                .Where(e => e.Department != null)
                .Select(e => e.Department!)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            return View(query.OrderBy(e => e.Name).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View(new AddEmployeeDTO { Name = "", Email = "" });

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(AddEmployeeDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Salary = dto.Salary,
                Department = dto.Department,
                JobTitle = dto.JobTitle,
                HiredOn = DateTime.UtcNow
            };

            _db.Employees.Add(employee);
            _db.SaveChanges();

            TempData["Success"] = $"{employee.Name} was added successfully.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id)
        {
            var emp = _db.Employees.Find(id);
            if (emp is null) return NotFound();

            var dto = new UpdateEmployeeDTO
            {
                Name = emp.Name,
                Email = emp.Email,
                Phone = emp.Phone,
                Salary = emp.Salary,
                Department = emp.Department,
                JobTitle = emp.JobTitle
            };

            ViewBag.Id = id;
            ViewBag.HiredOn = emp.HiredOn;
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, UpdateEmployeeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(dto);
            }

            var emp = _db.Employees.Find(id);
            if (emp is null) return NotFound();

            emp.Name = dto.Name;
            emp.Email = dto.Email;
            emp.Phone = dto.Phone;
            emp.Salary = dto.Salary;
            emp.Department = dto.Department;
            emp.JobTitle = dto.JobTitle;

            _db.SaveChanges();

            TempData["Success"] = $"{emp.Name} was updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            var emp = _db.Employees.Find(id);
            if (emp is null) return NotFound();
            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var emp = _db.Employees.Find(id);
            if (emp is null) return NotFound();

            string name = emp.Name;
            _db.Employees.Remove(emp);
            _db.SaveChanges();

            TempData["Success"] = $"{name} was removed.";
            return RedirectToAction(nameof(Index));
        }
    }
}
