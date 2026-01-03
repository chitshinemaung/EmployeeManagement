using EmployeeManagementSystem.Database.EmployeeDB;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace EmployeeManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmployeeDBContxt _db;
        private readonly ILoginUserRepositories _loginUserRepositories;

        public LoginController(EmployeeDBContxt db, ILoginUserRepositories loginUserRepositories)
        {
            _db = db;
            _loginUserRepositories = loginUserRepositories;
        }


        public async Task<IActionResult> UserLoginIndex(LoginRequestModel requestModel)
        {
            return View();
        }

        public async Task<IActionResult> UserHome()
        {
            return View();
        }

        [Route("Login/UserLogin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(LoginRequestModel request)
        {
            //var loginUser = await _db.TblEmployees
            //    .FirstOrDefaultAsync(u => u.EmployeeEmail == request.Username && u.EmployeesId == request.Password);               

            var loginData = await _loginUserRepositories.permitUserAsync(request);
            //var loginData = JsonConvert.SerializeObject(loginUser);
            HttpContext.Session.SetString("Login", loginData);
            
            return RedirectToAction("UserHome");
            
        }
    }
}
