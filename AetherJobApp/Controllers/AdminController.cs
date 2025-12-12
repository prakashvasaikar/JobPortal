using System.Diagnostics;
using AetherJobApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AetherJobApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult UserList()
        {
            return View();
        }
        public IActionResult Vacancy()
        {
            return View();
        }
        public IActionResult Career()
        {
            return View();
        }
        public IActionResult Candidates()
        {
            return View();
        }
        public IActionResult Requirement()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
