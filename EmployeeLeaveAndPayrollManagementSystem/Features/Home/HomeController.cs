using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveAndPayrollManagementSystem.Features.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("/Features/Home/Views/Index.cshtml");
        }
    }
}
