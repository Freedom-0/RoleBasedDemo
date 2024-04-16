using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace RoleBasedDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Maker")]
        //[Authorize]
        //[Authorize(Policy = "RoleAndLocationPolicy")]
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}