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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StreamsOfSound.Controllers
{
    //[Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Volunteer, Admin")]
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
        public IActionResult CancelOpportunity(int Id)
        {
            var oppData = (from su in _context.InstrumentSignUp
                          join sl in _context.InstrumentsSlots on su.InstrumentSlotsId equals sl.Id
                          join o in _context.Opportunities on sl.OpportunityId equals o.Id
                          where su.Id == Id
                          select new
                          {
                              youRaiseMeUp = su,
                              slotoriusBIG = sl,
                              gelegenheit = o,
                          }).FirstOrDefault();

            var yeet = new ReasonToTeetViewModel();
            yeet.youRaiseMeUpId = oppData.youRaiseMeUp.Id;
            yeet.gelegenheitId = oppData.gelegenheit.Id;
            yeet.slotoriousBIGId = oppData.slotoriusBIG.Id;
            
            return View(yeet);
        }

        [HttpPost]
        public IActionResult YeetOpportunity(int Id)
        {

            return Redirect("MyOpportunities");
        }

        //[Authorize(Roles = "Volunteer")]
        [HttpPost]
        public IActionResult YeetSignUp(YeetSignUpRequest request)
        {
            var slotToCancel = _context.InstrumentsSlots.SingleOrDefault(x => x.Id == request.slotId);

            if (slotToCancel == null)
                return NotFound();

            var userToYeetOut = _context.InstrumentSignUp.SingleOrDefault(x => x.Id == request.signUpId);

            var toYeetOrToNotYeet = slotToCancel == null || userToYeetOut == null;
            if (toYeetOrToNotYeet)
                return NotFound();

            var yeet = new ReasonToYeet();
            yeet.YeetedSlotId = userToYeetOut.InstrumentSlotsId;
            yeet.ThisIsMyLastResort = request.ReasonToCancel;
            _context.ReasonToYeet.Add(yeet);

            _context.InstrumentSignUp.Remove(userToYeetOut);

            _context.SaveChanges();
            return Redirect("MyOpportunities");
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

        [Authorize(Roles = "Volunteer, Admin")]
        [HttpGet]
        public async Task<IActionResult> MyOpportunities()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var guidId = Guid.Parse(userId);

            var opportunitiesList = from o in _context.Opportunities
                                    join isl in _context.InstrumentsSlots on o.Id equals isl.OpportunityId
                                    join su in _context.InstrumentSignUp on isl.Id equals su.InstrumentSlotsId
                                    where su.UserId == guidId
                                    select new MyOpportuntiesViewModel 
                                    { 
                                        Id= o.Id,
                                        Name = o.Name,
                                        Description = o.Description,
                                        StartTime = o.StartTime,
                                        EndTime = o.EndTime,
                                        Address = o.Address,
                                        Address1 = o.Address1,
                                        City = o.City,
                                        State = o.State,
                                        Zip = o.Zip,
                                        Instrument = isl.Instrument,
                                        SlotStartTime = isl.StartTime,
                                        SlotEndTime = isl.EndTime,
                                        signUpId = su.Id

                                    };
            return View(opportunitiesList.ToList());
        }

        [Authorize(Roles = "Volunteer")]
        [HttpGet]
        public async Task<IActionResult> OpportunityList()
        {
            var opportunitiesList = _context.Opportunities.Where(x => !x.isArchived ?? false).ToList();
            var slotsList = _context.InstrumentsSlots.ToList();
            var model = new OpportunityStaffListViewModel();
            model.Opportunity = opportunitiesList;
            model.Slots = slotsList;
            return View(model);
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
            var slotsList = _context.InstrumentsSlots.ToList();
            var model = new OpportunityStaffListViewModel();
            model.Opportunity = opportunitiesList;
            model.Slots = slotsList;
            return View(model);
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

        [Authorize(Roles = "Admin")]
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
            foreach (var item in request.Slots)
            {
                item.OpportunityId = opportunityId;
                _context.InstrumentsSlots.Add(item);
                _context.SaveChanges();
            }
            return RedirectToAction("OpportunityStaffList");
        }

        [Authorize(Roles = "Admin")]
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
            ViewData["Slots"] = _context.InstrumentsSlots.Where(m => m.OpportunityId == opportunity.Id).ToList();

            return View(opportunity);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(CreateOpportunityRequest request)
        {
            if (request == null)
                return new JsonResult(BadRequest());

            var opportunity = _context.Opportunities.FirstOrDefault(m => m.Id == request.Id);

            if (opportunity == null)
            {
                return NotFound();

            }

            opportunity.Name = request.Name;
            opportunity.StartTime = request.StartTime;
            opportunity.EndTime = request.EndTime;
            opportunity.Description = request.Description;
            opportunity.Address1 = request.Address1;
            opportunity.Address = request.Address;
            opportunity.State = request.State;
            opportunity.City = request.City;
            opportunity.Zip = request.Zip;
            await _context.SaveChangesAsync();

            var slots = _context.InstrumentsSlots.Where(m => m.OpportunityId == request.Id);
            _context.InstrumentsSlots.RemoveRange(slots);
            var opportunityId = opportunity.Id;
            foreach (var item in request.Slots)
            {
                item.OpportunityId = opportunityId;
                _context.InstrumentsSlots.Add(item);
                _context.SaveChanges();
            }
            return RedirectToAction("OpportunityStaffList");
        }


        [Authorize(Roles = "Admin, Volunteer")]
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
            var signUps = (from o in _context.Opportunities
                           join isl in _context.InstrumentsSlots on o.Id equals isl.OpportunityId
                           join su in _context.InstrumentSignUp on isl.Id equals su.InstrumentSlotsId
                           where o.Id == Id
                           select new
                           {
                               youRaiseMeUp = su,
                           }).ToList();

            var slots = _context.InstrumentsSlots.Where(m => m.OpportunityId == opportunity.Id).ToList();
            var takenSignUps = signUps.Select(m=>m.youRaiseMeUp).Where(m=>slots.Any(x=>x.Id==m.InstrumentSlotsId)).ToList();
            var openSlots = slots.Where(m => !takenSignUps.Any(x => x.InstrumentSlotsId == m.Id)).ToList();
            ViewData["OpenSlots"] = openSlots;
            ViewData["Slots"] = slots;
            ViewData["SlotSignUp"] = signUps;   
            return View(opportunity);
        }

        [Authorize(Roles = "Admin")]
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
                return RedirectToAction("OpportunityStaffList");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ArchiveList()
        {
            var archivedOpportunities = _context.Opportunities.Where(x => x.isArchived ?? false).ToList();
            return View(archivedOpportunities);
        }

        [Authorize(Roles = "Volunteer, Admin")]
        [HttpPost]
        public async Task<ActionResult> ConfirmSignUp(VolunteerSignUpFormRequest signup)
        {

            var slot = await _context.InstrumentsSlots.FirstOrDefaultAsync(x => x.Id == signup.InstrumentSlotsId);
            var user = await _context.Users.FirstOrDefaultAsync(y => y.Id == signup.UserId);
            if (slot == null || user == null)
            {
                return NotFound("This opportunity or user is not found");
            }


            var signUpForSlot = signup.ToSignUp();

            _context.InstrumentSignUp.Add(signUpForSlot);

            await _context.SaveChangesAsync();
            //return View();
            return RedirectToAction("MyOpportunities", new { UserId = user.Id });
        }

        [Authorize(Roles = "Volunteer, Admin")]
        public async Task<ActionResult> PassingInSignUp(int Id, int OpportunityId)
        {
            // TODO: Check if request is null, return to an error page or error message,
            // if opp not null - update & obtain userID, verify that the user exists - context (similar to 23)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var guidId = Guid.Parse(userId);
            var opportunity = await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == OpportunityId);
            var instrumentSlot = await _context.InstrumentsSlots.FirstOrDefaultAsync(x => x.Id == Id);
            var user = await _context.Users.FirstOrDefaultAsync(y => y.Id == guidId);

            if (instrumentSlot == null || user == null)
            {
                return NotFound("This opportunity or user is not found");
            }

            var viewModel = new ConfirmSignUpViewModel()
            {
                OpportunityId = opportunity.Id,
                UserId = user.Id,
                Opportunity = opportunity,
                User = user,
                Slot = instrumentSlot,
            };
            return View("ConfirmSignUp", viewModel);
        }

    }
}