using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public class GetAllManagersHandler : IRequestHandler<GetAllManagersQuery, List<Employee>>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllManagersHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Employee>> Handle(GetAllManagersQuery request, CancellationToken cancellationToken)
        {
            var usersInManagerRole = await _userManager.GetUsersInRoleAsync("Manager");
            return await _context.Employees.Where(e => usersInManagerRole.Select(u => u.EmployeeId).Contains(e.Id)).ToListAsync();
        }
    }
}
