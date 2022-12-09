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
        public ActionResult SignUp(string txtName, string txtEmail, string txtInstrument, string txtPhone)
        {
            ViewBag.Name = txtName;
            ViewBag.Email = txtEmail;
            ViewBag.Phone = txtPhone;
            ViewBag.Instrument = txtInstrument;

            return View("MyOpportunities");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
