using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedDemo.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = "Maker, Checker")]
        public IActionResult CreateCustomer()
        {
            return View();
        }

       
    }
}
