using EmployeeLeaveAndPayrollManagementSystem.Features.Employees;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls
{
    public class Payroll
    {
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay { get; set; }

        public Employee Employee { get; set; }
        public List<Allowance> Allowances { get; set; }
        public List<Deduction> Deductions { get; set; }
    }
}
