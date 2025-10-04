using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Accounts.Commands
{
    public record LoginCommand(LoginViewModel userViewModel) : IRequest<bool>;
}
