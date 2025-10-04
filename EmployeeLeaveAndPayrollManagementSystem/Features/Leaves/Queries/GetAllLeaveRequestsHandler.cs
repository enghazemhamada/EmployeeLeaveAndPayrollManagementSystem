using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Leaves.Queries
{
    public class GetAllLeaveRequestsHandler : IRequestHandler<GetAllLeaveRequestsQuery, List<LeaveRequest>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public GetAllLeaveRequestsHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<LeaveRequest>> Handle(GetAllLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if(user.IsInRole("Admin"))
            {
                return await _context.LeaveRequests.Include(l => l.Employee).ToListAsync();
            }
            else if(user.IsInRole("Manager"))
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

                return await _context.LeaveRequests.Include(l => l.Employee).Where(l => l.Employee.ManagerId == appUser.EmployeeId).ToListAsync();
            }
            else
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

                return await _context.LeaveRequests.Include(l => l.Employee).Where(l => l.EmployeeId == appUser.EmployeeId).ToListAsync();
            }
        }
    }
}
