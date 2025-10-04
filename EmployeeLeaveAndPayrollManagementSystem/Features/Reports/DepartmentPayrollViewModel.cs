namespace EmployeeLeaveAndPayrollManagementSystem.Features.Reports
{
    public class DepartmentPayrollViewModel
    {
        public string Department { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalPayroll { get; set; }
    }
}
