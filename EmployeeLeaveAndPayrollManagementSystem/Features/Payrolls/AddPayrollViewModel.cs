using EmployeeLeaveAndPayrollManagementSystem.Features.Employees;
using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls
{
    public class AddPayrollViewModel
    {
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public List<AllowanceViewModel> Allowances { get; set; }
        public List<DeductionViewModel> Deductions { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
