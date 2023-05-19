using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamsOfSound.Models.Domain_Entities;
using System.Data;

namespace StreamsOfSound.Controllers
{
    public class RoleManagerController : Controller
    {
            private readonly RoleManager<IdentityRole> _roleManager;
            public RoleManagerController(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<IActionResult> ManageRoles()
            {
                var roles = await _roleManager.Roles.ToListAsync();
                return View(roles);
            }

            [HttpPost]
            public async Task<IActionResult> AddRole(string roleName)
            {
                if (roleName != null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
                }
                return RedirectToAction("Index");
            }
    }
}
