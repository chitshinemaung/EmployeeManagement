using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Database.EmployeeDB;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Database.EmployeementModel;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
       private readonly EmployeeDBContxt _db;

        public EmployeeController(EmployeeDBContxt db)
        {
            _db = db;
        }

        [ActionName("Index")]
        public async Task<IActionResult> EmpolyeeIndex()
        {
            var employees = await _db.TblEmployees
                .Where(e => e.EmployeeDeleteFlag == false)
                .OrderByDescending(e => e.EmployeesId)
                .ToListAsync();
            return View("EmpolyeeIndex", employees);
        }


        [ActionName("Create")]
        public async Task<IActionResult> CreateEmployee()
        {
            
            return View("CreateEmployee");
        }

        [ActionName("Save")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEmployee(CreateEmployeeModel employees)
        {
            var newEmployee = new TblEmployee
            {
                EmployeesId = Ulid.NewUlid().ToString().Substring(0,11),
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
            return RedirectToAction("Index");

            
        }

        [ActionName("Test")]
        public async Task<IActionResult> AutoGenerate()
        {
            
            for (int i = 0; i < 10; i++)
            {
                int randomNumber = new Random().Next(1000, 9999);
                int row = i + 1;
                var autoEmployee = new TblEmployee
                {
                    EmployeesId = Ulid.NewUlid().ToString().Substring(0, 11),
                    EmployeeFirstName = "Hello",
                    EmployeesLastName = "" + i,
                    EmployeeEmail = randomNumber + "@example.com",
                    EmployeePhone = "555-010" + row,
                    EmployeeDepartment = "Marketing",
                    EmployeePosition = "staff",
                    EmployeeSalary = 30000 + (row * 1000),
                    EmployeeHireDate = DateTime.Now.AddDays(-row * 30),
                    EmployeeDeleteFlag = false
                };
                await _db.TblEmployees.AddAsync(autoEmployee);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> EditEmployee(string id, CreateEmployeeModel em)
        {
            var employee = await _db.TblEmployees.FirstOrDefaultAsync(e=> e.EmployeesId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View("EditEmployee", employee);
        }

        [ActionName("Update")]
        public async Task<IActionResult> UpdateEmployee(string id, UpdateEmployeeModel uem)
        {
            var uemp = await _db.TblEmployees.FirstOrDefaultAsync(u => u.EmployeesId == id);
            if (uemp == null) return RedirectToAction("Index");

            uemp.EmployeeFirstName = uem.EmployeeFirstName;
            uemp.EmployeesLastName = uem.EmployeesLastName;
            uemp.EmployeeEmail = uem.EmployeeEmail;
            uemp.EmployeePhone = uem.EmployeePhone;
            uemp.EmployeeDepartment = uem.EmployeeDepartment;
            uemp.EmployeePosition = uem.EmployeePosition;
            uemp.EmployeeSalary = uem.EmployeeSalary;
            uemp.EmployeeHireDate = uem.EmployeeHireDate;
            uemp.EmployeeSalary = uem.EmployeeSalary;

            _db.TblEmployees.Update(uemp);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var demp = await _db.TblEmployees.FirstOrDefaultAsync(d => d.EmployeesId == id);
            if (demp == null) return RedirectToAction("Index");
            demp.EmployeeDeleteFlag = true;

            _db.TblEmployees.Update(demp);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RestoreData()
        {
            var resData = await _db.TblEmployees
                                    .Where(r => r.EmployeeDeleteFlag == true)
                                    .ToListAsync();
            return View(resData);
        }

        [ActionName("Restore")]
        public async Task<IActionResult> BackData(string id)
        {
            var restoreData = await _db.TblEmployees.FirstOrDefaultAsync(r => r.EmployeesId == id);
            if (restoreData == null) RedirectToAction("Index");

            restoreData.EmployeeDeleteFlag = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}