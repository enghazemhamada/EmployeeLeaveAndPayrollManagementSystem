using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Leaves.Commands
{
    public record AddLeaveRequestCommand(AddLeaveRequestViewModel leaveRequestVM) : IRequest<AjaxResponse>;
}
