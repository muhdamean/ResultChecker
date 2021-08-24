using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResultChecker.Models;
using ResultChecker.Services;

namespace ResultChecker.Pages.Admin
{
    public class ManageUserRolesModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ManageUserRolesModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [BindProperty(SupportsGet =true)]
        public UserRoles userRoles { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet =true)]
        public List<UserRoles> UserRolesList { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                TempData["message"] = $"User not found";
                return Page();
            }
            var roles = new List<UserRoles>();
            foreach (var role in roleManager.Roles.ToList())
            {
                var  userRolesList = new UserRoles
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesList.IsSelected = true;
                }
                else
                {
                    userRolesList.IsSelected = false;
                }
                UserRolesList.Add(userRolesList);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    TempData["message"] = $"User not found";
                    return Page();
                }
                var roles = await userManager.GetRolesAsync(user);
                var result = await userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Cannot remove user existing roles");
                    return Page();
                }
                result = await userManager.AddToRolesAsync(user, UserRolesList.Where(x => x.IsSelected).Select(y => y.RoleName));
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Cannot add user to roles");
                    return Page();
                }
                return RedirectToAction("editstaff", new { Id = Id });
            }
            return Page();
        }
    }
}
