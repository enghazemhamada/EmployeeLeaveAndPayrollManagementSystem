using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls
{
    public class Allowance
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("Payroll")]
        public int PayrollId { get; set; }

        public Payroll Payroll { get; set; }
    }
}
