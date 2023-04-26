using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using StreamsOfSound.Data;
using System.Text;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;
using StreamsOfSound.Models.Requests;
using StreamsOfSound.Models;
using System.Security.Claims;
using StreamsOfSound.Models.Domain_Entities;
using StreamsOfSound.Models.ViewModel;
using StreamsOfSound.Services;

namespace StreamsOfSound.Controllers
{

    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly ILogger<CreateNewStaffRequest> _logger;
        private readonly IEmailSender _emailSender;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<CreateNewStaffRequest> logger
            )
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RegisterNewStaff()
        {
            return View();
        }

        //[Authorize(Roles = "Super, Admin")]
        [HttpGet]
        public IActionResult ArchiveStaff()
        {
            var users = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where r.Name == "Admin" && u.Archived == true
                        select new { User = u };

            return View(users.Select(m => m.User).ToList());
        }

        //[Authorize(Roles = "Admin, Super")]
        [HttpGet]
        public IActionResult ArchiveVolunteers()
        {
            var users = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where r.Name == "Volunteer" && u.Archived == true
                        select new { User = u };

            return View(users.Select(m => m.User).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateInstrumentsAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var guidId = Guid.Parse(userId);
            var user = await _context.Users.FirstOrDefaultAsync(y => y.Id == guidId);

            if (user == null || user.Instruments == null)
            {
                return NotFound("This opportunity or user is not found");
            }

            var viewModel = new UpdateInstrumentsViewModel()
            {
                OldInstruments = user.Instruments,
                NewInstruments = user.Instruments,
                
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Volunteer")]
        [HttpPost]
        public async Task<IActionResult> UpdateInstruments(UpdateInstrumentsRequest request)
        {
            var userId = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(y => y.UserName == userId);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            user.Instruments = request.NewInstruments;
            _context.SaveChanges();

            var model = new UpdateInstrumentsViewModel
            {
                OldInstruments = user.Instruments,
                NewInstruments = user.Instruments,
                StatusMessage = "Instrument List has been updated!"
            };
            
            return View("UpdateInstruments", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterNewStaff(CreateNewStaffRequest staff, string returnUrl = null)
        {
            var user = new ApplicationUser();

            await _userStore.SetUserNameAsync(user, staff.Email, CancellationToken.None);

            user.FirstName = staff.FirstName;
            user.LastName = staff.LastName;
            user.Email = staff.Email;
            user.Position = staff.Position;
            user.EmailConfirmed = true;
            user.Archived = false;
            string password = PasswordGenerator.GeneratePassword();
            var result = await _userManager.CreateAsync(user, password);
            returnUrl ??= Url.Content("~/");

            if (result.Succeeded)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.AddToRoleAsync(user, "Admin"); 
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var staffCreatePasswordUrl = (Url.Action(
                    "CreateStaffPassword",
                    "Account",
                    new
                    {
                        email = staff.Email,
                        token = token
                    },Request.Scheme));

                await _emailSender.SendEmailAsync(staff.Email, "Set your Staff account password",
                $"Please set your staff account password " +
                $"<a href={staffCreatePasswordUrl}> by clicking the link</a>.");

                return View("ConfirmStaffPassword");
            }
            return View(staff);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateStaffPasswordAsync(string token, string email)
        {
            if(User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("CreateStaffPassword", "Account", new {token = token, email=email});   
            }
            
            if (email == null || token == null)
            {
                ModelState.AddModelError("", "invalid password reset token");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateStaffPassword(StaffPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);  
            if(user != null)
            {
                var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
                var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
                if(result.Succeeded)
                {
                    return View("ResetStaffPasswordConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return View("ResetStaffPasswordConfirmation");
        }

        //[Authorize(Roles = "Admin, Super")]
        [HttpGet]
        public async Task<IActionResult> ActiveVolunteerList()
        {
            var users = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where r.Name == "Volunteer"
                        select new { User = u }; 
            
            return View(users.Select(m=>m.User).ToList());
        }

        //[Authorize(Roles = "Volunteer, Admin")]
        [HttpGet]
        public async Task<IActionResult> ActiveStaffList()
        {
            var users = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where r.Name == "Admin" && u.Archived == false
                        select new { User = u };

            return View(users.Select(m => m.User).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> ObliterateVolunteer(Guid id)
        {
            // TODO: Tell them what happened
            if (id == Guid.Empty)
            {
                ModelState.AddModelError("", "No User Found");
                return View("Error");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                ModelState.AddModelError("", "No user found");
                return View("Error");
            }
            user.Archived = true;
            user.TimeEnd = DateTime.Now;
            await _context.SaveChangesAsync();

            ViewData["Message"] = $"User {user.UserName} has been archived.";

            return RedirectToAction("ArchiveVolunteers", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> ObliterateStaff(Guid id)
        {
            // TODO: Tell them what happened
            if (id == Guid.Empty)
            {
                ModelState.AddModelError("", "No User Found");
                return View("Error");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                ModelState.AddModelError("", "No user found");
                return View("Error");
            }
            user.Archived = true;
            user.TimeEnd= DateTime.Now; 
            await _context.SaveChangesAsync();

            ViewData["Message"] = $"User {user.UserName} has been archived.";

            return RedirectToAction("ArchiveStaff", "Account");
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}