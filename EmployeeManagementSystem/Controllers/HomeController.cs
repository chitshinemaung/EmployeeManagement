using EmployeeManagementSystem.Database.EmployeeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public readonly EmployeeDBContxt _db;

        public HomeController(EmployeeDBContxt db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _db.TblEmployees.ToListAsync();

            var dashboardStats = new 
            {
                TotalEmployees = employees.Count,
                Departments = 8,
                NewThisMonth = 12,
                AvgSalary = 00000
            };

            ViewBag.DashboardStats = dashboardStats;
            return View();

        }
    }
}