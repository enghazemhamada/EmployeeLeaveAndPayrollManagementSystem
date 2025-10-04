using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public class GetEmployeeEditDataHandler : IRequestHandler<GetEmployeeEditDataQuery, EditEmployeeViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetEmployeeEditDataHandler(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<EditEmployeeViewModel> Handle(GetEmployeeEditDataQuery request, CancellationToken cancellationToken)
        {
            Employee employee = await _context.Employees.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == request.id);
            if(employee != null)
            {
                var usersInManagerRole = await _userManager.GetUsersInRoleAsync("Manager");
                EditEmployeeViewModel employeeVM = new EditEmployeeViewModel
                {
                    Id = request.id,
                    Name = employee.Name,
                    Department = employee.Department,
                    Position = employee.Position,
                    BaseSalary = employee.BaseSalary,
                    HireDate = employee.HireDate,
                    LeaveDate = employee.LeaveDate,
                    LeaveBalance = employee.LeaveBalance,
                    ManagerId = employee.ManagerId,
                    Role = (await _userManager.GetRolesAsync(employee.User)).FirstOrDefault(),

                    Managers = await _context.Employees.Where(e => usersInManagerRole.Select(u => u.EmployeeId).Contains(e.Id)).ToListAsync(),
                    Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync()
                };
                return employeeVM;
            }
            return null;
        }
    }
}
