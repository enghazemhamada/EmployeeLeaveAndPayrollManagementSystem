using EmployeeLeaveAndPayrollManagementSystem.Features.Employees;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Leaves
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveType Type { get; set; }
        public string Reason { get; set; }
        public LeaveStatus Status { get; set; }

        public Employee Employee { get; set; }
    }

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum LeaveType
    {
        Annual,
        Sick,
        Unpaid,
        Other
    }
}
