using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using StreamsOfSound.Data;
//using StreamsOfSound.Models.Domain_Entities;
//using StreamsOfSound.Models.Requests;
using StreamsOfSounds.Data;
using StreamsOfSounds.Models;
using StreamsOfSounds.Services;

namespace StreamsOfSound.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        //added email
        private readonly IEmailSender _emailSender;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender
            )
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult RegisterNewStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStaff(CreateNewStaff staff)
        {
            var user = new ApplicationUser();

            await _userStore.SetUserNameAsync(user, staff.Email, CancellationToken.None);
            
            await _emailSender.SendEmailAsync(staff.Email, "new staff signed up", "Body of Email");
            // TODO: If there are a number of props for this then consider a method
            // SetUserProps(ApplicationUser user, RegisterStaffRequest request)
            // Mainly to remove the logic from the controller's action method.
            user.FirstName = staff.FirstName;
            user.LastName = staff.LastName;
            user.Email = staff.Email;
            var result = await _userManager.CreateAsync(user, null);

            // TODO: What happens if there are errors creating the account?

            return View(staff);
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