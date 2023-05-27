using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using StreamsOfSound.Data;
using StreamsOfSound.Models;
using StreamsOfSound.Models.ViewModel;
using System.Data;

namespace StreamsOfSound.Controllers
{
    //[Authorize(Roles = "Super")]
    public class UserRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ApplicationDbContext _context;
        public UserRoleController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> StaffRoleUsers()
        {

           var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<StaffRoleUserViewModel>();

            if(users == null)
            {
                return NotFound();
            }

            foreach (ApplicationUser user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("Admin") || userRoles.Contains("Super"))
                {
                    var thisViewModel = new StaffRoleUserViewModel();
                    thisViewModel.UserId = user.Id;
                    thisViewModel.Email = user.Email;
                    thisViewModel.FirstName = user.FirstName;
                    thisViewModel.LastName = user.LastName;
                    thisViewModel.Roles = await GetUserRoles(user);
                    userRolesViewModel.Add(thisViewModel);
                }
            }
            return View(userRolesViewModel);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public async Task<IActionResult> ManageStaffRole(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageStaffRoleViewModel>();
            var allRoles = _roleManager.Roles.ToList(); 
            foreach (var role in allRoles)
            {
                var userRolesViewModel = new ManageStaffRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                userRolesViewModel.Selected = await _userManager.IsInRoleAsync(user, role.Name);
               
                model.Add(userRolesViewModel);
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ManageStaffRole(List<ManageStaffRoleViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("StaffRoleUsers");
        }
    }
}
