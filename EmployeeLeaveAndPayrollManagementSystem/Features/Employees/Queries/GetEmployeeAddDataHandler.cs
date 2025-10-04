using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public class GetEmployeeAddDataHandler : IRequestHandler<GetEmployeeAddDataQuery, AddEmployeeViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetEmployeeAddDataHandler(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<AddEmployeeViewModel> Handle(GetEmployeeAddDataQuery request, CancellationToken cancellationToken)
        {
            var usersInManagerRole = await _userManager.GetUsersInRoleAsync("Manager");
            AddEmployeeViewModel employeeVM = new AddEmployeeViewModel
            {
                Managers = await _context.Employees.Where(e => usersInManagerRole.Select(u => u.EmployeeId).Contains(e.Id)).ToListAsync(),
                Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync()
            };
            return employeeVM;
        }
    }
}
