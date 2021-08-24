using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResultChecker.Models;

namespace ResultChecker.Pages.Staff
{
    public class DeleteStaffModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DeleteStaffModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [BindProperty(SupportsGet = true)]
        public ApplicationUser Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var data = await userManager.FindByIdAsync(Id);
            if (data == null)
            {
                TempData["message"] = $"User not found";
                return RedirectToPage("staff");
            }
            Users = await userManager.Users.FirstOrDefaultAsync(u => u.Id == Id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                TempData["message"] = $"User not found";
                return RedirectToPage("staff");
            }
            var claims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (result.Succeeded)
            {
                var removeRoles = await userManager.RemoveFromRolesAsync(user, roles);
                if (removeRoles.Succeeded)
                {
                    var delete = await userManager.DeleteAsync(user);
                    if (delete.Succeeded)
                    {
                        TempData["message"] = $"User not found";
                        return RedirectToPage("../page/staff");
                    }
                }
                foreach (var error in removeRoles.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Page();
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return Page();
        }
    }
}
