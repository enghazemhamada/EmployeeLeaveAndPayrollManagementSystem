using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public class GetEmployeeDeleteDataHandler : IRequestHandler<GetEmployeeDeleteDataQuery, Employee>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetEmployeeDeleteDataHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Employee> Handle(GetEmployeeDeleteDataQuery request, CancellationToken cancellationToken)
        {
            Employee employee = await _context.Employees.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == request.id);
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(employee != null && employee.User.Id != userId)
                return employee;

            return null;
        }
    }
}
