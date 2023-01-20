using Microsoft.AspNetCore.Mvc;
using StreamsOfSounds.Models;
using System.Diagnostics;

namespace StreamsOfSounds.Controllers
{
    public class VolunteerSignUpController : Controller
    {
        [HttpPost]
        public ActionResult form1(string txtName, string txtEmail, string txtInstrument)
        {
            ViewBag.Name = txtName;
            ViewBag.Email = txtEmail;
            ViewBag.Instrument = txtInstrument;

            return View("Date");
        }

        [HttpPost]
        public ActionResult SignUp(VolunteerSignUpFormRequest request)
        {
            // TODO.MR: Create the object for the database, add to database, return good view, and validation
            ViewBag.FirstName = request.FirstName; 
            ViewBag.LastName = request.LastName;
            ViewBag.Email = request.Email;
            ViewBag.Phone = request.PhoneNumber;

            return View("MyOpportunities");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
