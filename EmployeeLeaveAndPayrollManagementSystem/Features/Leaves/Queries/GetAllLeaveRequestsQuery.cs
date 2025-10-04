using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Leaves.Queries
{
    public record GetAllLeaveRequestsQuery : IRequest<List<LeaveRequest>>;
}
