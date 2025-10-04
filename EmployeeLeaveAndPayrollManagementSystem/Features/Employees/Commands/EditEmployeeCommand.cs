using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Commands
{
    public record EditEmployeeCommand(int id, EditEmployeeViewModel employeeVM) : IRequest<bool>;
}
