using Microsoft.AspNetCore.Mvc;
using StreamsOfSounds.Models;
using System.Diagnostics;

namespace StreamsOfSounds.Controllers
{
    public class SignUpVolunteer : Controller
    {
        private readonly ILogger<SignUpVolunteer> _logger;

        public SignUpVolunteer(ILogger<SignUpVolunteer> logger)
        {
            _logger = logger;
        }
        public IActionResult MyOpportunityList()
        {
            return View();
        }
    }
}
