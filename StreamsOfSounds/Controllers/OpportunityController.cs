using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StreamsOfSound.Data;
using StreamsOfSound.Models;
using StreamsOfSound.Models.Domain_Entities;
using StreamsOfSound.Models.Requests;
using System.Security.Claims;
using StreamsOfSound.Models.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using System.Net;

namespace StreamsOfSound.Controllers
{
    public class OpportunityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OpportunityController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ConfirmSignUp()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CancelOpportunity()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateOpportunity()
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

            //[HttpGet]
            //public ActionResult EditOpportunity()
            //{ }

            //[HttpPost]
            //public ActionResult EditOpportunity()
            //{ }

            [HttpGet]
        public async Task<ActionResult> DeleteOpportunity(int Id)
        {
            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == Id);

            if (Id == default(int))
            {
                return new JsonResult(BadRequest());
            }

            if (opportunity == null)
            {
                return NotFound();
            }

            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();

            return RedirectToAction("OpportunityList");
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmSignUp(VolunteerSignUpFormRequest signup)
        {

            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == signup.OpportunityId);
            var user = await _context.Users.FirstOrDefaultAsync(y => y.Id == signup.UserId);
            if (opportunity == null || user == null)
            {
                return NotFound("This opportunity or user is not found");
            }

            //var signUpOpportunity = signup.ToSignUp();
            //_context.SignUpForOpportunities.Add(signUpOpportunity);
            //await _context.SaveChangesAsync();
            //var signUpOpportunity = signup.ToSignUp();
            //_context.SignUpForOpportunities.Add(signUpOpportunity);
            var ConfirmSignUpViewModel = new ConfirmSignUpViewModel()
            {
                Opportunity = opportunity,
                UserId = user
            };
            return View("MyOpportunities",ConfirmSignUpViewModel);

        }
        public async Task<ActionResult> PassingInSignUp(VolunteerSignUpFormRequest request)
        {
            // TODO: Check if request is null, return to an error page or error message,
            // if opp not null - update & obtain userID, verify that the user exists - context (similar to 23)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var guidId = Guid.Parse(userId);
            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == request.OpportunityId);
            var user = await _context.Users.FirstOrDefaultAsync(y => y.Id == guidId);
            if (opportunity == null || user == null)
            {
                return NotFound("This opportunity or user is not found");
            }
            var viewModel = new ConfirmSignUpViewModel()
            {
                Opportunity = opportunity,
                UserId = user
            };
            return View("ConfirmSignUp", viewModel);
        }

    }
}