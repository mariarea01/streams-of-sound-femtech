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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var guidId = Guid.Parse(userId);
            var user = await _context.Users.FirstOrDefaultAsync(y => y.Id == guidId);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.Instruments = request.NewInstruments;
            _context.Users.Add(user);
            _context.SaveChanges();

            var model = new UpdateInstrumentsViewModel
            {
                OldInstruments = user.Instruments,
                NewInstruments = user.Instruments
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
            await _signInManager.SignOutAsync();
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
            await _signInManager.SignOutAsync();
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

        /*
        private async Task LoadAsync(ApplicationUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            user.Email = email;

            var input = new UpdateInstrumentsRequest()
            {
                //NewInstruments = request.,
            };
        }
        */

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ActiveVolunteerList()
        {
            var users = await _context.Users.ToListAsync();
            //var volunteers = _userManager.GetUsersInRoleAsync("Volunteer");

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
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

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
          
            return RedirectToAction(nameof(ActiveVolunteerList));
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