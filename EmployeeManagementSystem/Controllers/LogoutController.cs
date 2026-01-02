using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class LogoutController : Controller
    {

        [Route("Logout/Index")]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.Clear();
            return Redirect("/Index/Login");
        }
    }
}
