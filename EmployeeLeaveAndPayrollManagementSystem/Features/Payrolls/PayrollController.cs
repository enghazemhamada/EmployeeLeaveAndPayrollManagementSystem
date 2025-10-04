using EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries;
using EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls.Commands;
using EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Payrolls
{
    public class PayrollController : Controller
    {
        private readonly IMediator _mediator;

        public PayrollController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Add()
        {
            AddPayrollViewModel payrollVM = await _mediator.Send(new GetPayrollAddDataQuery());
            return View("/Features/Payrolls/Views/Add.cshtml", payrollVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Add(AddPayrollViewModel payrollVM)
        {
            if(ModelState.IsValid)
            {
                bool result = await _mediator.Send(new AddPayrollCommand(payrollVM));
                if(result)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Failed to create payroll");
            }
            payrollVM.Employees = await _mediator.Send(new GetAllEmployeesInEmployeeRoleQuery());
            return View("/Features/Payrolls/Views/Add.cshtml", payrollVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,HR,Employee")]
        public async Task<IActionResult> Index()
        {
            List<Payroll> payrolls = await _mediator.Send(new GetAllPayrollsQuery());
            return View("/Features/Payrolls/Views/Index.cshtml", payrolls);
        }
    }
}
