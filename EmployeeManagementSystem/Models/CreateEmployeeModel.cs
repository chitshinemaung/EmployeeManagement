namespace EmployeeManagementSystem.Models
{
    public class CreateEmployeeModel
    {
        public string EmployeesId { get; set; }

        public string EmployeeFirstName { get; set; } = null!;

        public string EmployeesLastName { get; set; } = null!;

        public string EmployeeEmail { get; set; } = null!;

        public string EmployeePhone { get; set; } = null!;

        public string EmployeeDepartment { get; set; } = null!;

        public string EmployeePosition { get; set; } = null!;

        public decimal? EmployeeSalary { get; set; }

        public DateTime? EmployeeHireDate { get; set; }

        public bool? EmployeeDeleteFlag { get; set; }
    }
}
