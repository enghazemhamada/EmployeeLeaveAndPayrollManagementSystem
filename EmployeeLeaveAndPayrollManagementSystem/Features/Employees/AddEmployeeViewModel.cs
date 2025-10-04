using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees
{
    public class AddEmployeeViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }

        [Display(Name = "Base Salary")]
        public decimal BaseSalary { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Leave Balance")]
        public int LeaveBalance { get; set; }

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }
        public string Role { get; set; }

        public List<Employee>? Managers { get; set; }
        public List<string>? Roles { get; set; }
    }
}
