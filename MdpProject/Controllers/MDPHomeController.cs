using System.Diagnostics;
using MdpProject.Models;
using Microsoft.AspNetCore.Mvc;
using MdpProject.Models;

namespace project2.Controllers
{
    public class MDPHomeController : Controller
    {
        private readonly ILogger<MDPHomeController> _logger;

        public MDPHomeController(ILogger<MDPHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
