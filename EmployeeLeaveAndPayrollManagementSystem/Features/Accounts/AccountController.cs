using EmployeeLeaveAndPayrollManagementSystem.Features.Accounts.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Accounts
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IMediator mediator, SignInManager<ApplicationUser> signInManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("/Features/Accounts/Views/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                bool result = await _mediator.Send(new LoginCommand(userViewModel));
                if(result)
                    return RedirectToAction("Index", "Home");

                ModelState.AddModelError("", "Email Or Password Wrong!");
            }
            return View("/Features/Accounts/Views/Login.cshtml", userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LoginOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
