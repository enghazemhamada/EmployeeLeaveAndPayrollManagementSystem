using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public record GetEmployeeEditDataQuery(int id) : IRequest<EditEmployeeViewModel>;
}
