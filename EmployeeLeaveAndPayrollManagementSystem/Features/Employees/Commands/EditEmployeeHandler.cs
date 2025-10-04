using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Commands
{
    public class EditEmployeeHandler : IRequestHandler<EditEmployeeCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditEmployeeHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _context.Employees.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == request.id);
            if(employee != null)
            {
                employee.Name = request.employeeVM.Name;
                employee.Department = request.employeeVM.Department;
                employee.Position = request.employeeVM.Position;
                employee.BaseSalary = request.employeeVM.BaseSalary;
                employee.HireDate = request.employeeVM.HireDate;
                employee.LeaveDate = request.employeeVM.LeaveDate;
                employee.LeaveBalance = request.employeeVM.LeaveBalance;
                employee.ManagerId = request.employeeVM.Role == "Employee" ? request.employeeVM.ManagerId : null;

                await _context.SaveChangesAsync();

                if(await _userManager.IsInRoleAsync(employee.User, request.employeeVM.Role))
                {
                    return true;
                }
                await _userManager.RemoveFromRoleAsync(employee.User, (await _userManager.GetRolesAsync(employee.User)).FirstOrDefault());
                await _userManager.AddToRoleAsync(employee.User, request.employeeVM.Role);
                return true;
            }
            return false;
        }
    }
}
