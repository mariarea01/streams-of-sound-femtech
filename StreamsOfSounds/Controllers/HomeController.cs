using Microsoft.AspNetCore.Mvc;
using StreamsOfSounds.Models;
using System.Diagnostics;

namespace StreamsOfSounds.Controllers
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

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
>>>>>>> StaffFixOpportunities
        {
            return View();
        }

        public IActionResult MyOpportunities()
        {
            return View();
        }

        public IActionResult OpportunityList()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

    }
}