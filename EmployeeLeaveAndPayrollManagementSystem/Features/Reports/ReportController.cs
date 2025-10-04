using EmployeeLeaveAndPayrollManagementSystem.Features.Reports.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Reports
{
    public class ReportController : Controller
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ReportsViewModel reportsVM = await _mediator.Send(new GetReportsQuery());
            return View("/Features/Reports/Views/Index.cshtml", reportsVM);
        }
    }
}
