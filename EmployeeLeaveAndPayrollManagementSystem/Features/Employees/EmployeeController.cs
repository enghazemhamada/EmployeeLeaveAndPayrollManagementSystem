using EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Commands;
using EmployeeLeaveAndPayrollManagementSystem.Features.Employees.Queries;
using EmployeeLeaveAndPayrollManagementSystem.Features.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Employees
{
    public class EmployeeController : Controller
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            AddEmployeeViewModel employeeVM = await _mediator.Send(new GetEmployeeAddDataQuery());
            return View("/Features/Employees/Views/Add.cshtml", employeeVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(AddEmployeeViewModel employeeVM)
        {
            if(ModelState.IsValid)
            {
                Employee employee = await _mediator.Send(new AddEmployeeCommand(employeeVM));
                return RedirectToAction("Index");
            }
            employeeVM.Managers = await _mediator.Send(new GetAllManagersQuery());
            employeeVM.Roles = await _mediator.Send(new GetAllRolesQuery());
            return View("/Features/Employees/Views/Add.cshtml", employeeVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _mediator.Send(new GetAllEmployeesQuery());
            return View("/Features/Employees/Views/Index.cshtml", employees);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            EditEmployeeViewModel employeeVM = await _mediator.Send(new GetEmployeeEditDataQuery(id));
            if(employeeVM != null)
                return View("/Features/Employees/Views/Edit.cshtml", employeeVM);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, EditEmployeeViewModel employeeVM)
        {
            if(ModelState.IsValid)
            {
                bool result = await _mediator.Send(new EditEmployeeCommand(id, employeeVM));
                if(result)
                    return RedirectToAction("Index");

                return NotFound();
            }
            employeeVM.Managers = await _mediator.Send(new GetAllManagersQuery());
            employeeVM.Roles = await _mediator.Send(new GetAllRolesQuery());
            return View("/Features/Employees/Views/Edit.cshtml", employeeVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Employee employee = await _mediator.Send(new GetEmployeeDeleteDataQuery(id));
            if(employee != null)
                return View("/Features/Employees/Views/Delete.cshtml", employee);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            bool result = await _mediator.Send(new DeleteEmployeeCommand(id));
            if(result)
                return RedirectToAction("Index");

            return NotFound();
        }
    }
}
