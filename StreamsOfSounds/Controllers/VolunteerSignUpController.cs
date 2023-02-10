using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using StreamsOfSounds.Data;
using StreamsOfSounds.Models;
using System.Diagnostics;

namespace StreamsOfSounds.Controllers
{
    public class VolunteerSignUpController : Controller
    { 
        private readonly ApplicationDbContext _context;

        public VolunteerSignUpController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> SignUp(VolunteerSignUpFormRequest request)
        {
            var userId = request.UserId;
            var oppId = request.OppId;
            var opportunity =  await _context.Opportunities.FirstOrDefaultAsync(x => x.Id == oppId);
            //Check if opp is null, return to an error page or error message, if opp not null - update & obtain userID, verify that the user exists - context (similar to 23)
            if(opportunity == null)
            {
                return NotFound("Product not found");
            }
            else { 
            var user = await _context.Opportunities.FirstOrDefaultAsync(y => y.UserId == userId);
            }

            return View("MyOpportunities");
        }

    }
}
