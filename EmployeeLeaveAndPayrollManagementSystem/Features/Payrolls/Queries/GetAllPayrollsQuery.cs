using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls.Queries
{
    public record GetAllPayrollsQuery : IRequest<List<Payroll>>;
}
