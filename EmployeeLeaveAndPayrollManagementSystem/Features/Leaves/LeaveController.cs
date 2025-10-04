using EmployeeLeaveAndPayrollManagementSystem.Features.Leaves.Commands;
using EmployeeLeaveAndPayrollManagementSystem.Features.Leaves.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Leaves
{
    public class LeaveController : Controller
    {
        private readonly IMediator _mediator;

        public LeaveController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult Add()
        {
            return View("/Features/Leaves/Views/Add.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Add(AddLeaveRequestViewModel leaveRequestVM)
        {
            if(ModelState.IsValid)
            {
                AjaxResponse response = await _mediator.Send(new AddLeaveRequestCommand(leaveRequestVM));
                if(response.Success)
                    return Ok(response);

                return BadRequest(response);
            }
            return BadRequest(new AjaxResponse { Success = false, Message = "Invalid data" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Index()
        {
            List<LeaveRequest> leaveRequests = await _mediator.Send(new GetAllLeaveRequestsQuery());
            return View("/Features/Leaves/Views/Index.cshtml", leaveRequests);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            AjaxResponse response = await _mediator.Send(new UpdateStatusCommand(id, status));
            if(response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
