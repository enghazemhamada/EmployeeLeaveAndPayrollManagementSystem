using EmployeeLeaveAndPayrollManagementSystem.Features.Employees;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls.Commands
{
    public class AddPayrollHandler : IRequestHandler<AddPayrollCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public AddPayrollHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddPayrollCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Employee employee = await _context.Employees.FindAsync(request.payrollVM.EmployeeId);
                if(employee == null)
                    return false;

                decimal totalAllowances = request.payrollVM.Allowances.Sum(a => a.Amount);
                decimal totalDeductions = request.payrollVM.Deductions.Sum(d => d.Amount);
                decimal netPay = employee.BaseSalary + totalAllowances - totalDeductions;

                Payroll payroll = new Payroll
                {
                    EmployeeId = employee.Id,
                    Month = request.payrollVM.Month,
                    Year = request.payrollVM.Year,
                    BaseSalary = employee.BaseSalary,
                    TotalAllowances = totalAllowances,
                    TotalDeductions = totalDeductions,
                    NetPay = netPay,
                    Allowances = request.payrollVM.Allowances.Select(a => new Allowance { Type = a.Type, Amount = a.Amount }).ToList(),
                    Deductions = request.payrollVM.Deductions.Select(d => new Deduction { Type = d.Type, Amount = d.Amount }).ToList()
                };

                await _context.Payrolls.AddAsync(payroll);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
