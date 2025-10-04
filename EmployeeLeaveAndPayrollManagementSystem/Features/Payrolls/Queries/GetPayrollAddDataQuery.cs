using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls.Queries
{
    public record GetPayrollAddDataQuery : IRequest<AddPayrollViewModel>;
}
