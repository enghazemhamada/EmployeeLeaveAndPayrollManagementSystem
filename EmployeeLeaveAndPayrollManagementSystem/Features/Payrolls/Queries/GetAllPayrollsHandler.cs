using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls.Queries
{
    public class GetAllPayrollsHandler : IRequestHandler<GetAllPayrollsQuery, List<Payroll>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllPayrollsHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<List<Payroll>> Handle(GetAllPayrollsQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext.User;
            if(user.IsInRole("Admin") || user.IsInRole("HR"))
            {
                return await _context.Payrolls.Include(p => p.Employee).ToListAsync();
            }
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return await _context.Payrolls.Include(p => p.Employee).Where(p => p.EmployeeId == appUser.EmployeeId).ToListAsync();
        }
    }
}
