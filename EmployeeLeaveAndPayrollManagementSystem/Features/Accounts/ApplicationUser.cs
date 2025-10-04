using EmployeeLeaveAndPayrollManagementSystem.Features.Employees;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Accounts
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
