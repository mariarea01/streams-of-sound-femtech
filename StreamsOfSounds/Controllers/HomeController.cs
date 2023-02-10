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

        public IActionResult CreateOpportunity()
        {
            return View();
        }
<<<<<<< HEAD
        public IActionResult MyOpportunityList()
        {
            return View();
        }

        public IActionResult Form(string eventName, DateTime dateTime, string duration, string address, string state, string city, int zip, int numOfVolunteers, bool paid, bool unpaid, string paidAmount)
        {

            ViewBag.eventName = eventName;
            ViewBag.date = dateTime;
            ViewBag.duration = duration;
            ViewBag.address = address;
            ViewBag.state = state;
            ViewBag.city = city;
            ViewBag.zip = zip;
            ViewBag.numOfVolunteers = numOfVolunteers;


            if (paid == null)
                ViewBag.Paid = "Unpaid";
            else
                ViewBag.Paid = "Paid";
            ViewBag.paidAmount = paidAmount;

            return View("ViewOpportunity");

        }

        public IActionResult ViewOpportunity()
        {
            return View();
        }


        public IActionResult Date()
=======

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