using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResultChecker.Models;
using ResultChecker.Services;

namespace ResultChecker.Pages.Admin
{
    public class ManageUserClaimsModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ManageUserClaimsModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<UserClaim> UserClaimList { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                TempData["message"] = $"User not found";
                return RedirectToPage("editStaff", new { Id = Id });
            }
            var existingUserClaims = await userManager.GetClaimsAsync(user);
            var userClaims = new List<UserClaim>();
            foreach (Claim claim in ClaimsStore.Claims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };
                if (existingUserClaims.Any(c => c.Type == claim.Type && c.Value == "true"))
                {
                    userClaim.IsSelected = true;
                }
                UserClaimList.Add(userClaim);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                TempData["message"] = $"User not found";
                return RedirectToPage("editStaff", new { Id = Id });
            }
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return Page();
            }
            result = await userManager.AddClaimsAsync(user, UserClaimList.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected user to claims");
                return Page();
            }
            return RedirectToPage("editStaff", new { Id = Id });
        }
    }
}
