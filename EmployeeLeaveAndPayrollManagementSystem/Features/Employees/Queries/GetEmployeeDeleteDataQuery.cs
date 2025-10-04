using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public record GetEmployeeDeleteDataQuery(int id) : IRequest<Employee>;
}
