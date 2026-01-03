
using EmployeeManagementSystem.Database.EmployeeDB;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Repositories
{
    public class LoginUserRepositories : ILoginUserRepositories

    {
        private readonly EmployeeDBContxt _db;
        public LoginUserRepositories(EmployeeDBContxt db)
        {
            _db = db;
        }
        public async Task<string> permitUserAsync(LoginRequestModel request)
        {
            var loginUser = await _db.TblEmployees
                .FirstOrDefaultAsync(u => u.EmployeeEmail == request.Username && u.EmployeesId == request.Password);
            var permitData = JsonConvert.SerializeObject(loginUser);

            return permitData;
        }
    }
}
