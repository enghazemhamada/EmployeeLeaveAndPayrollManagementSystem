using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, List<Employee>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllEmployeesHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<List<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext.User;
            if(user.IsInRole("Admin"))
            {
                return await _context.Employees.Include(e => e.Manager).ToListAsync();
            }
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            int managerId = appUser.EmployeeId;
            return await _context.Employees.Where(e => e.ManagerId == managerId).Include(e => e.Manager).ToListAsync();
        }
    }
}
