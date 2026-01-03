using EmployeeManagementSystem.Database.EmployeementModel;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<TblEmployee>> GetAllEmployeesAsync();
        Task CreateEmployeeAsync(CreateEmployeeModel createEmployeeModel);
        Task<TblEmployee?> GetEmployeeByIdAsync(string firstname, string id);
        Task<TblEmployee?> EditEmployeeByIdAsync(string id);
        Task<TblEmployee> UpdateEmployeeAsync(UpdateEmployeeModel uem);
        Task<TblEmployee> DeleteEmployeeByIdAsync(string id);
        Task<TblEmployee> RestoreEmployeeByIdAsync(string id);

    }
}
