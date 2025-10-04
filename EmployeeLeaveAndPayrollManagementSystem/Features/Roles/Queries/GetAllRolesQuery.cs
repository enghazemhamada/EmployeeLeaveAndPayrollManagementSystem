using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Roles.Queries
{
    public record GetAllRolesQuery : IRequest<List<string>>;
}
