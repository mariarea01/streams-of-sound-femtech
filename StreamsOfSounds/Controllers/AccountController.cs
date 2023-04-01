using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
//using StreamsOfSound.Data;
//using StreamsOfSound.Models.Domain_Entities;
//using StreamsOfSound.Models.Requests;
using StreamsOfSounds.Data;
using StreamsOfSounds.Models;
using StreamsOfSounds.Services;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.Extensions.Logging;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;
using StreamsOfSounds.Models.Requests;

namespace StreamsOfSound.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly ILogger<CreateNewStaff> _logger;
        private readonly IEmailSender _emailSender;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<CreateNewStaff> logger
            )
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult RegisterNewStaff()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> RegisterNewStaff(CreateNewStaff staff, string returnUrl = null)
        {
            var user = new ApplicationUser();

            await _userStore.SetUserNameAsync(user, staff.Email, CancellationToken.None);

            //await _emailSender.SendEmailAsync(staff.Email, "new staff signed up", "Body of Email");
            user.FirstName = staff.FirstName;
            user.LastName = staff.LastName;
            user.Email = staff.Email;
            user.EmailConfirmed = true;
            staff.Password = "123Abc!";
            var result = await _userManager.CreateAsync(user, staff.Password);
            returnUrl ??= Url.Content("~/");

            if (result.Succeeded)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

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
        public IActionResult CreateStaffPassword(string token, string email)
        { 
            if(email == null || token == null)
            {
                ModelState.AddModelError("", "invalid password reset token");
            }
            return View();
        }

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

        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            var users = await _context.Users.ToListAsync();

            return View(users);
        }


        [HttpPut]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // TODO: Tell them what happened
            if (id == null)
            {
                return new JsonResult(BadRequest());
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            // TODO: Tell them what happened (i.e. No user found)
            if (user == null)
            {
                ModelState.AddModelError("", "No user found");
                return View("Error");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(UsersList));
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