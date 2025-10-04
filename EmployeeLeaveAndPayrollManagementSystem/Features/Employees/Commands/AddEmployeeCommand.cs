using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Commands
{
    public record AddEmployeeCommand(AddEmployeeViewModel employeeVM) : IRequest<Employee>;
}
