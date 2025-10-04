namespace EmployeeLeaveAndPayrollManagementSystem.Features.Reports
{
    public class ReportsViewModel
    {
        public List<EmployeeLeaveBalanceViewModel> LeaveBalances { get; set; }
        public List<DepartmentPayrollViewModel> DepartmentPayrolls { get; set; }
        public List<AttendanceRecordViewModel> BestAttendance { get; set; }
        public List<SalaryTrendViewModel> SalaryTrends { get; set; }
        public List<LeaveTypeCountViewModel> LeaveTypeCounts { get; set; }
        public List<DepartmentAbsenceViewModel> DepartmentAbsences { get; set; }
    }
}
