using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls.Queries
{
    public class GetPayrollAddDataHandler : IRequestHandler<GetPayrollAddDataQuery, AddPayrollViewModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public GetPayrollAddDataHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<AddPayrollViewModel> Handle(GetPayrollAddDataQuery request, CancellationToken cancellationToken)
        {
            var usersInEmployeeRole = await _userManager.GetUsersInRoleAsync("Employee");
            var employeeIds = usersInEmployeeRole.Select(u => u.EmployeeId).ToList();

            return new AddPayrollViewModel
            {
                Employees = await _context.Employees.Where(e => employeeIds.Contains(e.Id)).ToListAsync()
            };
        }
    }
}
