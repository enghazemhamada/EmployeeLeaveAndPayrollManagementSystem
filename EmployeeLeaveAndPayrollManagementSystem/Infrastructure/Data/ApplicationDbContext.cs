using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Features.Employees;
using EmployeeLeaveAndPayrollManagementSystem.Features.Leaves;
using EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<Deduction> Deductions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "HR", NormalizedName = "HR" },
                new IdentityRole { Id = "3", Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole { Id = "4", Name = "Employee", NormalizedName = "EMPLOYEE" }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            List<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Hazem Hamada", Department = "IT", Position = "Admin", BaseSalary = 0, HireDate = DateTime.Now, LeaveBalance = 0 },
                new Employee { Id = 2, Name = "Ahmed Manager", Department = "Sales", Position = "Sales Manager", BaseSalary = 15000, HireDate = DateTime.Now, LeaveBalance = 20 },
                new Employee { Id = 3, Name = "Omar Employee", Department = "Support", Position = "Support Engineer", BaseSalary = 8000, HireDate = DateTime.Now, LeaveBalance = 15, ManagerId = 2 },
                new Employee { Id = 4, Name = "Sara HR", Department = "HR", Position = "HR Specialist", BaseSalary = 12000, HireDate = DateTime.Now, LeaveBalance = 18 }
            };
            builder.Entity<Employee>().HasData(employees);

            var hasher = new PasswordHasher<ApplicationUser>();

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "10", UserName = "admin@test.com", NormalizedUserName = "ADMIN@TEST.COM", Email = "admin@test.com", NormalizedEmail = "ADMIN@TEST.COM", EmailConfirmed = true, EmployeeId = 1, SecurityStamp = Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "Admin@123") },
                new ApplicationUser { Id = "11", UserName = "manager@test.com", NormalizedUserName = "MANAGER@TEST.COM", Email = "manager@test.com", NormalizedEmail = "MANAGER@TEST.COM", EmailConfirmed = true, EmployeeId = 2, SecurityStamp = Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "Manager@123") },
                new ApplicationUser { Id = "12", UserName = "employee@test.com", NormalizedUserName = "EMPLOYEE@TEST.COM", Email = "employee@test.com", NormalizedEmail = "EMPLOYEE@TEST.COM", EmailConfirmed = true, EmployeeId = 3, SecurityStamp = Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "Employee@123") },
                new ApplicationUser { Id = "13", UserName = "hr@test.com", NormalizedUserName = "HR@TEST.COM", Email = "hr@test.com", NormalizedEmail = "HR@TEST.COM", EmailConfirmed = true, EmployeeId = 4, SecurityStamp = Guid.NewGuid().ToString("D"), PasswordHash = hasher.HashPassword(null, "Hr@123") }
            };
            builder.Entity<ApplicationUser>().HasData(users);

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { UserId = "10", RoleId = "1" },
                new IdentityUserRole<string> { UserId = "11", RoleId = "3" },
                new IdentityUserRole<string> { UserId = "12", RoleId = "4" },
                new IdentityUserRole<string> { UserId = "13", RoleId = "2" }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);

            List<Payroll> payrolls = new List<Payroll>
            {
                new Payroll { Id = 1, EmployeeId = 3, Month = 9, Year = 2025, BaseSalary = 15000, TotalAllowances = 2000, TotalDeductions = 500, NetPay = 16500 }
            };
            builder.Entity<Payroll>().HasData(payrolls);

            List<Allowance> allowances = new List<Allowance>
            {
                new Allowance { Id = 1, PayrollId = 1, Type = "Bonus", Amount = 1000 },
                new Allowance { Id = 2, PayrollId = 1, Type = "Commission", Amount = 1000 }
            };
            builder.Entity<Allowance>().HasData(allowances);

            List<Deduction> deductions = new List<Deduction>
            {
                new Deduction { Id = 1, PayrollId = 1, Type = "Tax", Amount = 300 },
                new Deduction { Id = 2, PayrollId = 1, Type = "Insurance", Amount = 200 }
            };
            builder.Entity<Deduction>().HasData(deductions);

            List<LeaveRequest> leaveRequests = new List<LeaveRequest>
            {
                new LeaveRequest { Id = 1, EmployeeId = 3, StartDate = new DateTime(2025, 9, 1), EndDate = new DateTime(2025, 9, 5), Type = LeaveType.Annual, Reason = "Family vacation", Status = LeaveStatus.Pending }
            };
            builder.Entity<LeaveRequest>().HasData(leaveRequests);
        }
    }
}
