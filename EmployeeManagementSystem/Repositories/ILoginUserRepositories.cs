using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositories
{
    public interface ILoginUserRepositories
    {
        Task<string> permitUserAsync(LoginRequestModel request);
    }
}
