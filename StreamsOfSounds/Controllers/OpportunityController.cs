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
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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

        [Authorize(Roles = "Volunteer")]
        [HttpGet]
        public IActionResult ConfirmSignUp()
        {
            return View();
        }

        [Authorize(Roles = "Volunteer")]
        [HttpGet]
        public IActionResult CancelOpportunity()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            /*
            var model = new CreateOpportunityRequest();
            model.Slots = new List<InstrumentsSlots>();
            model.Slots.Add(new InstrumentsSlots
            {
                Instrument = "handpan",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            });

            model.Slots.Add(new InstrumentsSlots
            {
                Instrument = "bazooka",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            });
            */
            return View();

        }

        [Authorize(Roles = "Volunteer")]
        [HttpGet]
        public async Task<IActionResult> MyOpportunities()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var guidId = Guid.Parse(userId);

            var opportunitiesList = await _context.SignUpForOpportunities
                .Include(x => x.Opportunity)
                .Where(x => x.UserId == guidId)
                .ToListAsync();

            return View(opportunitiesList);
        }

        [Authorize(Roles = "Volunteer")]
        [HttpGet]
        public IActionResult OpportunityList()
        {
            var opportunitiesList = _context.Opportunities.Where(x => !x.isArchived??false).ToList();
            return View(opportunitiesList);
        }

        [Authorize(Roles = "Volunteer")]
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult OpportunityStaffList()
        {
            var opportunitiesList = _context.Opportunities.Where(x => !x.isArchived ?? false).ToList();
            return View(opportunitiesList);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult OpportunityStaffList(int? id)
        {
            if (id == null)
                return new JsonResult(BadRequest());

            var opportunitiesList = _context.Opportunities.SingleOrDefault(x => x.Id == id);

            if (opportunitiesList == null)
                return NotFound();

            return View(opportunitiesList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOpportunityRequest request)
        {
            if (request == null)
                return new JsonResult(BadRequest());

            var opportunity = request.ToOpportunity();
            opportunity.isArchived = false;
            _context.Opportunities.Add(opportunity);
            await _context.SaveChangesAsync();
            var opportunityId = opportunity.Id;
            foreach(var item in request.Slots)
            {
                item.OpportunityId = opportunityId;
                _context.InstrumentsSlots.Add(item);
                _context.SaveChanges();
            }
            return RedirectToAction("OpportunityStaffList");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
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
        //[HttpGet]
        //public ActionResult EditOpportunity()
        //{ }

            return RedirectToAction("OpportunityList");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            if (Id == default(int))
            {
                return new JsonResult(BadRequest());
            }

            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == Id);

            if (opportunity == null)
            {
                return NotFound();
            }

            return View(opportunity);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Opportunity opportunity)
        {
            _context.Opportunities.Update(opportunity);
            await _context.SaveChangesAsync();
            return RedirectToAction("OpportunityList");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int Id)
        {
            if (Id == default(int))
            {
                return new JsonResult(BadRequest());
            }

            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == Id);

            if (opportunity == null)
            {
                return NotFound();
            }

            return View(opportunity);
        }

        [HttpGet]
        public async Task<IActionResult> Archive(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);

            if (opportunity == null)
            {
                return NotFound();
            }

            opportunity.isArchived = !opportunity.isArchived;
            await _context.SaveChangesAsync();

            if (opportunity.isArchived ?? false)
            {
                return RedirectToAction("ArchiveList");
            }
            else
            {
                return RedirectToAction("OpportunityList");
            }
        }

        [HttpGet]
        public IActionResult ArchiveList()
        {
            var archivedOpportunities = _context.Opportunities.Where(x => x.isArchived ?? false).ToList();
            return View(archivedOpportunities);
        }

        [Authorize(Roles = "Volunteer")]
        [HttpPost]
        public async Task<ActionResult> ConfirmSignUp(VolunteerSignUpFormRequest signup)
        {

            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == signup.OpportunityId);
            var user = await _context.Users.FirstOrDefaultAsync(y => y.Id == signup.UserId);
            if (opportunity == null || user == null)
            {
                return NotFound("This opportunity or user is not found");
            }

            var signUpOpportunity = signup.ToSignUp();
            _context.SignUpForOpportunities.Add(signUpOpportunity);

            await _context.SaveChangesAsync();
            //return View();
            return RedirectToAction("MyOpportunities", new { UserId = user.Id });
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
                OpportunityId = opportunity.Id,
                UserId = user.Id,
                Opportunity = opportunity,
                User = user
            };
            //var signingup = request.ToSignUp();
            //_context.SignUpForOpportunities.Add(viewModel);
            //await _context.SaveChangesAsync();
            return View("ConfirmSignUp", viewModel);
        }

    }
}