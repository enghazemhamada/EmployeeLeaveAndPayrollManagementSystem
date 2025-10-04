using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls.Commands
{
    public record AddPayrollCommand(AddPayrollViewModel payrollVM) : IRequest<bool>;
}
