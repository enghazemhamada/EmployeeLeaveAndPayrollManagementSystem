using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Roles.Queries
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetAllRolesHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<string>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        }
    }
}
