using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamsOfSound.Data;
using StreamsOfSound.Models.Domain_Entities;
using StreamsOfSound.Models.Requests;

namespace StreamsOfSound.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult RegisterStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStaff(RegisterStaffRequest request)
        {
            var user = new ApplicationUser();

            await _userStore.SetUserNameAsync(user, request.Email, CancellationToken.None);
            // TODO: If there are a number of props for this then consider a method
            // SetUserProps(ApplicationUser user, RegisterStaffRequest request)
            // Mainly to remove the logic from the controller's action method.
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            var result = await _userManager.CreateAsync(user, request.Password);

            // TODO: What happens if there are errors creating the account?

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            var users = await _context.Users.ToListAsync();

            return View(users);
        }

        [HttpPut]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            // TODO: Tell them what happened
            if (id == Guid.Empty)
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