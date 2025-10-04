using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Leaves.Commands
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, AjaxResponse>
    {
        private readonly ApplicationDbContext _context;

        public UpdateStatusHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AjaxResponse> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _context.LeaveRequests.FindAsync(request.id);
            if(leaveRequest != null)
            {
                if(!Enum.TryParse<LeaveStatus>(request.status, out var newStatus))
                {
                    return new AjaxResponse { Success = false, Message = "Invalid status value" };
                }

                leaveRequest.Status = newStatus;
                await _context.SaveChangesAsync();
                return new AjaxResponse { Success = true, Message = $"Leave request {request.status}" };
            }
            return new AjaxResponse { Success = false, Message = "Leave request not found" };
        }
    }
}
