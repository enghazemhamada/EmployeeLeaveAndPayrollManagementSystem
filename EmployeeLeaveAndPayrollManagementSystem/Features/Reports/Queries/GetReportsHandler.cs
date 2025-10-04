using EmployeeLeaveAndPayrollManagementSystem.Features.Leaves;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Reports.Queries
{
    public class GetReportsHandler : IRequestHandler<GetReportsQuery, ReportsViewModel>
    {
        private readonly ApplicationDbContext _context;

        public GetReportsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReportsViewModel> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            ReportsViewModel reportsVM = new ReportsViewModel
            {
                LeaveBalances = await _context.Employees.Select(e => new EmployeeLeaveBalanceViewModel
                {
                    EmployeeName = e.Name,
                    LeaveBalance = e.LeaveBalance
                }).ToListAsync(),

                DepartmentPayrolls = await _context.Payrolls.GroupBy(p => new { p.Employee.Department, p.Month, p.Year })
                .Select(g => new DepartmentPayrollViewModel
                {
                    Department = g.Key.Department,
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    TotalPayroll = g.Sum(p => p.NetPay)
                }).ToListAsync(),

                BestAttendance = await _context.Employees.Select(e => new AttendanceRecordViewModel
                {
                    EmployeeName = e.Name,
                    Absences = _context.LeaveRequests.Count(l => l.EmployeeId == e.Id && l.Status == LeaveStatus.Approved)
                }).OrderBy(x => x.Absences).Take(5).ToListAsync(),

                SalaryTrends = await _context.Payrolls.GroupBy(p => new { p.Month, p.Year })
                .Select(g => new SalaryTrendViewModel
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    TotalPayroll = g.Sum(p => p.NetPay)
                }).OrderBy(x => x.Year).ThenBy(x => x.Month).ToListAsync(),

                LeaveTypeCounts = await _context.LeaveRequests.Where(l => l.Status == LeaveStatus.Approved).GroupBy(l => l.Type)
                .Select(g => new LeaveTypeCountViewModel
                {
                    LeaveType = g.Key.ToString(),
                    Count = g.Count()
                }).ToListAsync(),

                DepartmentAbsences = await _context.LeaveRequests.Where(l => l.Status == LeaveStatus.Approved).GroupBy(l => l.Employee.Department)
                .Select(g => new DepartmentAbsenceViewModel
                {
                    Department = g.Key,
                    AbsenceCount = g.Count()
                }).ToListAsync()
            };
            return reportsVM;
        }
    }
}
