using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StreamsOfSound.Data;
using StreamsOfSound.Models;
using Microsoft.EntityFrameworkCore;
using StreamsOfSound.Models.Domain_Entities;
using StreamsOfSound.Models.Requests;
using System.Net;

namespace StreamsOfSound.Controllers
{
    public class OpportunityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpportunityController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> OpportunityList()
        {
            var opportunitiesList = await _context.Opportunities.ToListAsync();

            return View(opportunitiesList);
        }

        [HttpPost]
        public IActionResult OpportunityList(int? id)
        {
            if (id == null)
                return new JsonResult(BadRequest());

            var opportunitiesList = _context.Opportunities.SingleOrDefault(x => x.Id == id);

            if (opportunitiesList == null)
                return NotFound();

            return View(opportunitiesList);
        }

        [HttpGet]
        public IActionResult CreateOpportunity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOpportunity(CreateOpportunityRequest request)
        {
            if (request == null)
                return new JsonResult(BadRequest());

            var opportunity = request.ToOpportunity();
            _context.Opportunities.Add(opportunity);
            await _context.SaveChangesAsync();

            return RedirectToAction("OpportunityList");
        }

        [HttpGet]
        public async Task<IActionResult> MyOpportunities(Guid userId)
        {
            var opportunitiesList = await _context.Opportunities.Where(x => x.UserId == userId).ToListAsync();

            opportunitiesList = new List<Opportunity>();

            opportunitiesList.Add(new Opportunity() { Name = "James" });
           
            return View(opportunitiesList);
        }

        //[HttpGet]
        //public ActionResult EditOpportunity()
        //{ }

        //[HttpPost]
        //public ActionResult EditOpportunity()
        //{ }

        [HttpGet]
        public async Task<ActionResult> DeleteOpportunity([FromQuery]int Id)
        {

            if (Id == null)
            {
                return new JsonResult(BadRequest());
            }
            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == Id);

            if (opportunity == null)
            {
                return NotFound();
            }

            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();

            return View("OpportunityList");
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(VolunteerSignUpFormRequest request)
        {
            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == request.OppId);
            var user = await _context.Opportunities.FirstOrDefaultAsync(y => y.UserId == request.UserId);
            if (opportunity == null || user == null)
            {
                return NotFound("PROVIDE BETTER ERROR MESSAGE HERE");
            }

            return View("MyOpportunities");
        }
    }
}