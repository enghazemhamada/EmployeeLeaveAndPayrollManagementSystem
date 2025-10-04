using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries
{
    public record GetEmployeeAddDataQuery : IRequest<AddEmployeeViewModel>;
}
