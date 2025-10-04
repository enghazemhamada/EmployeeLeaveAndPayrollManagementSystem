using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public class GetAllEmployeesInEmployeeRoleHandler : IRequestHandler<GetAllEmployeesInEmployeeRoleQuery, List<Employee>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public GetAllEmployeesInEmployeeRoleHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<Employee>> Handle(GetAllEmployeesInEmployeeRoleQuery request, CancellationToken cancellationToken)
        {
            var usersInEmployeeRole = await _userManager.GetUsersInRoleAsync("Employee");
            var employeeIds = usersInEmployeeRole.Select(u => u.EmployeeId).ToList();

            return await _context.Employees.Where(e => employeeIds.Contains(e.Id)).ToListAsync();
        }
    }
}
