using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Leaves
{
    public class AddLeaveRequestViewModel
    {
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public LeaveType Type { get; set; }
        public string Reason { get; set; }
    }
}
