using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StreamsOfSounds.Data;
using VolunteerWebApplication.Models;

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
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateOpportunity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOpportunity(Opportunities opportunity) {

            if (ModelState.IsValid)
            {
                _context.Opportunities.Add(opportunity);
                await _context.SaveChangesAsync();
            }

            return View("OpportunityList");
        }

        [HttpPost]
        public IActionResult OpportunityList()
        {
            var opportunitiesList = _context.Opportunities.ToList();

            return View(opportunitiesList);

        }

    }
}

