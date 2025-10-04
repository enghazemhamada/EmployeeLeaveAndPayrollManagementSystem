using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Commands
{
    public record DeleteEmployeeCommand(int id) : IRequest<bool>;
}
