using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Reports.Queries
{
    public record GetReportsQuery : IRequest<ReportsViewModel>;
}
