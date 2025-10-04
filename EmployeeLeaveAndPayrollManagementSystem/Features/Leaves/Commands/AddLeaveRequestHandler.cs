using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts;
using EmployeeLeaveAndPayrollManagementSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Leaves.Commands
{
    public class AddLeaveRequestHandler : IRequestHandler<AddLeaveRequestCommand, AjaxResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AddLeaveRequestHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _context = context;
        }

        public async Task<AjaxResponse> Handle(AddLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appUser = await _userManager.Users.Include(u => u.Employee).FirstOrDefaultAsync(u => u.Id == userId);

            int requestedDays = (request.leaveRequestVM.EndDate - request.leaveRequestVM.StartDate).Days + 1;
            if(appUser.Employee.LeaveBalance < requestedDays)
            {
                return new AjaxResponse
                {
                    Success = false,
                    Message = "Not enough leave balance"
                };
            }

            bool overlapping = await _context.LeaveRequests.AnyAsync(l => l.EmployeeId == appUser.EmployeeId &&
            ((request.leaveRequestVM.StartDate >= l.StartDate && request.leaveRequestVM.StartDate <= l.EndDate) ||
            (request.leaveRequestVM.EndDate >= l.StartDate && request.leaveRequestVM.EndDate <= l.EndDate)));

            if(overlapping)
            {
                return new AjaxResponse
                {
                    Success = false,
                    Message = "Leave request overlaps with an existing one"
                };
            }

            LeaveRequest leaveRequest = new LeaveRequest
            {
                EmployeeId = appUser.EmployeeId,
                StartDate = request.leaveRequestVM.StartDate,
                EndDate = request.leaveRequestVM.EndDate,
                Type = (LeaveType)request.leaveRequestVM.Type,
                Reason = request.leaveRequestVM.Reason,
                Status = LeaveStatus.Pending
            };

            await _context.LeaveRequests.AddAsync(leaveRequest);
            appUser.Employee.LeaveBalance -= requestedDays;
            await _context.SaveChangesAsync();

            return new AjaxResponse
            {
                Success = true,
                Message = "Leave request submitted successfully"
            };
        }
    }
}
