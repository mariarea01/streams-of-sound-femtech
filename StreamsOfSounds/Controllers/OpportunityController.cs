using Microsoft.AspNetCore.Mvc;
using StreamsOfSound.Data;
using StreamsOfSound.Models;
using Microsoft.EntityFrameworkCore;
using StreamsOfSound.Models.Domain_Entities;
using StreamsOfSound.Models.Requests;

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
        public IActionResult OpportunityList()
        {
            // var opportunitiesList = _context.Opportunities.Where(x => !x.isArchived).ToList();
            var opp = new Opportunity
            {
                Id = 1,
                Name = "Test",
                Description = "Random Text",
                StartTime = new DateTimeOffset(),
                EndTime = new DateTimeOffset(),
                Address = "New Address",
                City = "My City",
                State = "Florida",
                Zip = "32258",
                SlotsAvailable = 10,
                SlotsOpenings = 2,
                isArchived = false
            };

            var opp1 = new Opportunity
            {
                Id = 2,
                Name = "Second",
                Description = "Second Text",
                StartTime = new DateTimeOffset(),
                EndTime = new DateTimeOffset(),
                Address = "7237 Corklan Dr",
                City = "Jacksonville",
                State = "Florida",
                Zip = "32258",
                SlotsAvailable = 10,
                SlotsOpenings = 2,
                isArchived = false
            };

            var opp2 = new Opportunity
            {
                Id = 3,
                Name = "Third",
                Description = "Third Text",
                StartTime = new DateTimeOffset(),
                EndTime = new DateTimeOffset(),
                Address = "120 Brand Rd",
                City = "Santa Rosa",
                State = "California",
                Zip = "95409",
                SlotsAvailable = 5,
                SlotsOpenings = 0,
                isArchived = false
            };

            var opp3 = new Opportunity
            {
                Id = 4,
                Name = "Fourth",
                Description = "Fourth text",
                StartTime = new DateTimeOffset(),
                EndTime = new DateTimeOffset(),
                Address = "5454 Clifton Rd",
                City = "Jacksonville",
                State = "Florida",
                Zip = "32211",
                SlotsAvailable = 1,
                SlotsOpenings = 1,
                isArchived = false
            };

            var oppList = new List<Opportunity>();
            oppList.Add(opp);
            oppList.Add(opp1);
            oppList.Add(opp2);
            oppList.Add(opp3);

            return View(oppList);
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
        public IActionResult Create()
        {
            return View();
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<DataTableResponse>> GetOpportunities()
        {
            var products = await _context.Opportunities.ToListAsync();

            return new DataTableResponse
            {
                OpportunitiesTotal = products.Count(),
                OpportunitiesFiltered = 10,
                Data = products.ToArray()
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOpportunityRequest request)
        {
            if (request == null)
                return new JsonResult(BadRequest());

            var opportunity = request.ToOpportunity();
            _context.Opportunities.Add(opportunity);
            await _context.SaveChangesAsync();

            return RedirectToAction("OpportunityList");
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

            opportunity.StartTime = opportunity.StartTime.ToLocalTime();
            opportunity.EndTime = opportunity.EndTime.ToLocalTime();

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

            return RedirectToAction("ArchiveList");
        }

        [HttpGet]
        public IActionResult ArchiveList()
        {
            var archivedOpportunities = _context.Opportunities.Where(x => x.isArchived).ToList();
            return View(archivedOpportunities);
        }
    }
}