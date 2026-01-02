using EmployeeManagementSystem.Database.EmployeeDB;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmployeeDBContxt _db;

        public LoginController(EmployeeDBContxt db)
        {
            _db = db;
        }

        public async Task<IActionResult> UserLogin()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("Login/UserLogin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(LoginRequestModel request)
        {
            var loginUser = await _db.TblEmployees
                .FirstOrDefaultAsync(u => u.EmployeeEmail == request.Username && u.EmployeesId == request.Password);                  

                var loginData = JsonConvert.SerializeObject(loginUser);
                HttpContext.Session.SetString("Login", loginData);
                return RedirectToAction("Index");
        }
    }
}
