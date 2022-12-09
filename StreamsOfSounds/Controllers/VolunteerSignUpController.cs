using Microsoft.AspNetCore.Mvc;
using StreamsOfSounds.Models;

namespace StreamsOfSounds.Controllers
{
    public class VolunteerSignUpController : Controller
    {
        public ActionResult GetEmployeeData()
        {
            List<VolunteerSignUp> vol = new List<VolunteerSignUp>
            {
                new VolunteerSignUp
                {
                    EmployeeId = 1,
                    EmployeeName = "John",
                    Address = "12 Fremont St. Clermont, FL 2813",
                    Phone = "+1-234-2838421"
                },
                new VolunteerSignUp
                {
                    EmployeeId = 2,
                    EmployeeName = "Smith",
                    Address = "14 Highland Drive Fort Worth, TX 3994",
                    Phone = "+1-234-2244521"
                },
                new VolunteerSignUp
                {
                    EmployeeId = 3,
                    EmployeeName = "Marry",
                    Address = "23 Fremont Road Milledgeville, GA 6788",
                    Phone = "+1-234-46568421"
                }
            };

            ViewBag.VolunteerSignUp = vol;

            return View();
        }

    }
}
