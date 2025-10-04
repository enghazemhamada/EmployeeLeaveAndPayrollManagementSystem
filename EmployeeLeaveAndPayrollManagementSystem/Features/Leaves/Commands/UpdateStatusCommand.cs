using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Leaves.Commands
{
    public record UpdateStatusCommand(int id, string status) : IRequest<AjaxResponse>;
}
