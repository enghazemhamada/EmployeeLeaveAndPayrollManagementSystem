using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public record GetAllManagersQuery : IRequest<List<Employee>>;
}
