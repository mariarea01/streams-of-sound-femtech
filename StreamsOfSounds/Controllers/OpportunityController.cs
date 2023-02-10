using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StreamsOfSounds.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreamsOfSounds.Controllers
{
    public class OpportunityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpportunityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOpportunities()
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

            return View("ViewOpportunities");

        }

        public async Task<IActionResult> ViewOpportunities()
        {
            var opportunitiesList = await _context.Opportunities.ToListAsync();
            return View(opportunitiesList);
        }

    }
}

