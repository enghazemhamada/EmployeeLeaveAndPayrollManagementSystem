using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public record GetAllEmployeesInEmployeeRoleQuery : IRequest<List<Employee>>;
}
