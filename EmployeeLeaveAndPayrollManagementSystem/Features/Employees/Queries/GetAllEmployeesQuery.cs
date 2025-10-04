using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public record GetAllEmployeesQuery : IRequest<List<Employee>>;
}
