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

            Employee adminEmployee = new Employee
            {
                Id = 1,
                Name = "Hazem Hamada",
                Department = "IT",
                Position = "Admin",
                BaseSalary = 100,
                HireDate = DateTime.Now,
                LeaveBalance = 20
            };
            builder.Entity<Employee>().HasData(adminEmployee);

            var hasher = new PasswordHasher<ApplicationUser>();
            ApplicationUser adminUser = new ApplicationUser
            {
                Id = "10",
                UserName = "admin@test.com",
                NormalizedUserName = "ADMIN@TEST.COM",
                Email = "admin@test.com",
                NormalizedEmail = "ADMIN@TEST.COM",
                EmailConfirmed = true,
                EmployeeId = 1,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            builder.Entity<ApplicationUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = "10",
                RoleId = "1"
            });
        }
    }
}
