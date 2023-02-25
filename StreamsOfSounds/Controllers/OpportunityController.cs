using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StreamsOfSound.Data;
using StreamsOfSound.Models.Domain_Entities;
using StreamsOfSound.Models.Requests;

namespace StreamsOfSound.Controllers
{
    public class OpportunityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OpportunityController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

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
        public async Task<IActionResult> CreateOpportunity(CreateOpportunityRequest request)
        {
            if (request == null)
                return new JsonResult(BadRequest());

            var opportunity = request.ToOpportunity();

            _context.Opportunities.Add(opportunity);
            await _context.SaveChangesAsync();

            return View("OpportunityList");
        }

        [HttpGet]
        public async Task<IActionResult> MyOpportunities(Guid userId)
        {
            // TODO: Update your view to match the logic of OpportunityList
            //var opportunitiesList = await _context.Opportunities.Where(x => x.UserId == userId).ToListAsync();

            var oppList = new List<Opportunity>();
            for (var i = 0; i < 8; i++)
            {
                oppList.Add(new Opportunity() { Name = "James" });
            }

            return View(oppList);
        }

        [HttpGet]
        public async Task<IActionResult> OpportunityList()
        {
            //var opportunitiesList = await _context.Opportunities.ToListAsync();

            var oppList = new List<Opportunity>();
            for (var i = 0; i < 8; i++)
            {
                oppList.Add(new Opportunity() { Name = "James" });
            }

            return View(oppList);
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(VolunteerSignUpFormRequest request)
        {
            // TODO: Check if request is null, return to an error page or error message,
            // if opp not null - update & obtain userID, verify that the user exists - context (similar to 23)
            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == request.OppId);
            var user = await _context.Opportunities.FirstOrDefaultAsync(y => y.UserId == request.UserId);
            if (opportunity == null || user == null)
            {
                return NotFound("This opportunity or user is not found");
            }
            else
            {
                var signUp = new VolunteerSignUpFormRequest()
                {
                    OppId = request.OppId,
                    UserId = request.UserId,
                };
                _context.Add(signUp);
                _context.SaveChanges();
                return View("MyOpportunities");
            }

            //return View("MyOpportunities");
        }
        public async Task<ActionResult> ConfirmSignUp()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
    }
}