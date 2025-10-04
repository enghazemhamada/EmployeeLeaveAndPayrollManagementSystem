using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Features.Leaves;
using EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }

        [Display(Name = "Base Salary")]
        public decimal BaseSalary { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Leave Date")]
        public DateTime? LeaveDate { get; set; }

        [Display(Name = "Leave Balance")]
        public int LeaveBalance { get; set; }

        [ForeignKey("Manager")]
        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }

        public Employee Manager { get; set; }
        public List<Employee> Team { get; set; }
        public ApplicationUser User { get; set; }
        public List<Payroll> Payrolls { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
