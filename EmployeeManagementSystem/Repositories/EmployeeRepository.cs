using EmployeeManagementSystem.Database.EmployeeDB;
using EmployeeManagementSystem.Database.EmployeementModel;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeManagementSystem.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly EmployeeDBContxt _db;
        public EmployeeRepository(EmployeeDBContxt db)
        {
            _db = db;
        }
        public async Task<List<TblEmployee>> GetAllEmployeesAsync()
        {
            var employees = await _db.TblEmployees
                .Where(e => e.EmployeeDeleteFlag == false)
                .OrderByDescending(e => e.EmployeesId)
                .ToListAsync();

            return employees;
        }

        public async Task CreateEmployeeAsync(CreateEmployeeModel employees)
        {
            var newEmployee = new TblEmployee
            {
                EmployeesId = Ulid.NewUlid().ToString().Substring(0, 11),
                EmployeeFirstName = employees.EmployeeFirstName,
                EmployeesLastName = employees.EmployeesLastName,
                EmployeeEmail = employees.EmployeeEmail,
                EmployeePhone = employees.EmployeePhone,
                EmployeeDepartment = employees.EmployeeDepartment,
                EmployeePosition = employees.EmployeePosition,
                EmployeeSalary = employees.EmployeeSalary,
                EmployeeHireDate = employees.EmployeeHireDate,
                EmployeeDeleteFlag = false
            };
            await _db.TblEmployees.AddAsync(newEmployee);
            await _db.SaveChangesAsync();
        }

        public Task<TblEmployee?> GetEmployeeByIdAsync(string firstname, string id)
        {
            var employee = _db.TblEmployees
                .FirstOrDefaultAsync(e => e.EmployeeFirstName == firstname || e.EmployeesId == id && e.EmployeeDeleteFlag == false);

            return employee;
        }

        public async Task<TblEmployee?> EditEmployeeByIdAsync(string id)
        {
            var employee = await _db.TblEmployees
                .Where(e => e.EmployeeDeleteFlag == false)
                .FirstOrDefaultAsync(e => e.EmployeesId == id);
            if (employee != null)
            {
                _db.TblEmployees.Update(employee);
                await _db.SaveChangesAsync();
                
            }
            return employee;
        }

        public async Task<TblEmployee> UpdateEmployeeAsync(UpdateEmployeeModel uemployee)
        {
            //var employee = _db.TblEmployees
            //    .Where(e => e.EmployeeDeleteFlag == false)
            //    .Select(e => new UpdateEmployeeModel
            //    {
            //        EmployeesId = e.EmployeesId,
            //        EmployeeFirstName = e.EmployeeFirstName,
            //        EmployeesLastName = e.EmployeesLastName,
            //        EmployeeEmail = e.EmployeeEmail,
            //        EmployeePhone = e.EmployeePhone,
            //        EmployeeDepartment = e.EmployeeDepartment,
            //        EmployeePosition = e.EmployeePosition,
            //        EmployeeSalary = e.EmployeeSalary,
            //        EmployeeHireDate = e.EmployeeHireDate
            //    })
            //    .FirstOrDefaultAsync(e => e.EmployeesId == id);

            var employee = await _db.TblEmployees
                .FirstOrDefaultAsync(e => e.EmployeesId == uemployee.EmployeesId);
            if (employee == null) return null!;
            if (employee != null)
            {
                employee.EmployeeFirstName = uemployee.EmployeeFirstName;
                employee.EmployeesLastName = uemployee.EmployeesLastName;
                employee.EmployeeEmail = uemployee.EmployeeEmail;
                employee.EmployeePhone = uemployee.EmployeePhone;
                employee.EmployeeDepartment = uemployee.EmployeeDepartment;
                employee.EmployeePosition = uemployee.EmployeePosition;
                employee.EmployeeSalary = uemployee.EmployeeSalary;
                employee.EmployeeHireDate = uemployee.EmployeeHireDate;
                employee.EmployeeDeleteFlag = false;
                _db.TblEmployees.Update(employee);
                await _db.SaveChangesAsync();
            }
            
             return employee!;
        }

        public async Task<TblEmployee> DeleteEmployeeByIdAsync(string id)
        {
            var delemployee = await _db.TblEmployees
                .Where(e => e.EmployeeDeleteFlag == false)
                .FirstOrDefaultAsync(e => e.EmployeesId == id);
            
            if (delemployee != null)
            {
                delemployee.EmployeeDeleteFlag = true;
                _db.TblEmployees.Update(delemployee);
                await _db.SaveChangesAsync();
            }
            return delemployee;
        }

        public async Task<TblEmployee> RestoreEmployeeByIdAsync(string id)
        {
            var resemployee = await _db.TblEmployees
                .Where(e=> e.EmployeeDeleteFlag == true)
                .FirstOrDefaultAsync (e => e.EmployeesId == id);

            if(resemployee != null)
            {
                resemployee.EmployeeDeleteFlag = false;
                _db.TblEmployees.Update(resemployee);
                await _db.SaveChangesAsync();
            }
            return resemployee;
        }
    }
}
