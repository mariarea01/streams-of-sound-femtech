using Microsoft.AspNetCore.Mvc;
using StreamsOfSounds.Models;
using System.Diagnostics;

namespace StreamsOfSounds.Controllers
{
    public class SignUpVolunteer : Controller
    {
        public IActionResult MyOpportunityList()
        {
            return View();
        }
    }
}
