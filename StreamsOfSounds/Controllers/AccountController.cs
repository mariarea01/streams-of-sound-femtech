using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StreamsOfSounds.Data;
using StreamsOfSounds.Models;

namespace StreamsOfSounds.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        //
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        /*private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IEmailSender _emailSender;*/


        //passed all 
        public AccountController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager)
        {

            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            //_emailStore = GetEmailStore(); causes error rn
            _signInManager = signInManager;
            //_emailSender = emailSender;
        }


        #region EmailMethod


        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> RegisterNewStaff(CreateNewStaff staff)
        {
            var user = new ApplicationUser();

            await _userStore.SetUserNameAsync(user, staff.Email, CancellationToken.None);
            //await _emailStore.SetEmailAsync(user, staff.Email, CancellationToken.None);
            user.FirstName = staff.FirstName;
            user.LastName = staff.LastName;
            var result = await _userManager.CreateAsync(user, staff.Password);
            return View(staff);

        }


        [HttpGet]
        public async Task<IActionResult> RegisterNewStaff()
        {
            var model = new CreateNewStaff();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            var users = await _context.Users.ToListAsync();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {

            //var user = await _context.Users.FindAsync(id);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                return View("Error");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UsersList));

            #region ExtraResearchStuff
            //cool region # comment out
            //control H to change all variable names at once 
            //cntrol kd to indent nicely

            /*
             * [ x ]grab stuff from register logic code
             * [ x ]make view for new staff member 
             * [ x ]navbar for staff member view form
             * [ x ]make model for staff
             *      it wants me to add confirm password and password???
             *      [  ] how to blur out the passwords with little dots
             *      
             * 
             * 
             * wTf is return Activator.CreateInstance<ApplicationUser>();????????????????????????????
             *          oh its just user = new ApplicationUser()
             *          
             * 
             * 
             * [ x ]see if db adds user info
             */

            #endregion
        }
    }
}


