using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Commands
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, Employee>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddEmployeeHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Employee> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = new Employee
            {
                Name = request.employeeVM.Name,
                Department = request.employeeVM.Department,
                Position = request.employeeVM.Position,
                BaseSalary = request.employeeVM.BaseSalary,
                HireDate = request.employeeVM.HireDate,
                LeaveBalance = request.employeeVM.LeaveBalance,
                ManagerId = request.employeeVM.Role == "Employee" ? request.employeeVM.ManagerId : null
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            ApplicationUser user = new ApplicationUser
            {
                Email = request.employeeVM.Email,
                UserName = request.employeeVM.Email,
                EmployeeId = employee.Id
            };

            IdentityResult result = await _userManager.CreateAsync(user, "Password_12345");
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.employeeVM.Role);
            }
            return employee;
        }
    }
}
