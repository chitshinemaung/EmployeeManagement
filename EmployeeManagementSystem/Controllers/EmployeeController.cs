using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Database.EmployeeDB;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Database.EmployeementModel;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
       private readonly EmployeeDBContxt _db;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(EmployeeDBContxt db, IEmployeeRepository employeeRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
        }

        [Route("Employee/Index")]
        [ActionName("Index")]
        public async Task<IActionResult> EmpolyeeIndex()
        {
            
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return View("EmpolyeeIndex", employees);
        }


        [ActionName("Create")]
        public async Task<IActionResult> CreateEmployee()
        {
            
            return View("CreateEmployee");
        }

        [Route("Employee/Save")]
        [ActionName("Save")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEmployee(CreateEmployeeModel employees)
        {
            await _employeeRepository.CreateEmployeeAsync(employees);
            return RedirectToAction("Index");
        }
       

        [ActionName("Edit")]
        public async Task<IActionResult> EditEmployee(string id)
        {
            var employee = await _employeeRepository.EditEmployeeByIdAsync(id);
            return View("EditEmployee", employee);
        }

        [ActionName("Update")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeModel uem)
        {
            await _employeeRepository.UpdateEmployeeAsync(uem);
            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            _employeeRepository.DeleteEmployeeByIdAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RestoreData()
        {
            var resData = await _db.TblEmployees.Where(r => r.EmployeeDeleteFlag == true).ToListAsync();
            return View(resData);
        }

        [ActionName("Restore")]
        public async Task<IActionResult> BackData(string id)
        {
            _employeeRepository.RestoreEmployeeByIdAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Search")]
        [HttpGet]
        public async Task<IActionResult> SearchEmployee(string firstname, string id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(firstname, id);
            return View("EmpolyeeIndex", new List<TblEmployee> { employee });
        }
    }
}