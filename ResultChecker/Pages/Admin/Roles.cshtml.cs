using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ResultChecker.Services;

namespace ResultChecker.Pages.Admin
{
    public class RolesModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger<CreateRoleModel> logger;
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesModel(AppDbContext dbContext, ILogger<CreateRoleModel> logger, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.roleManager = roleManager;
        }
        public List<IdentityRole> Roles { get; set; }
        public void OnGet()
        {
           Roles = roleManager.Roles.ToList();
        }
        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var role =await roleManager.FindByIdAsync(Id);
            if (role==null)
            {
                TempData["message"] = $"Role not found";
                return RedirectToPage("roles");
            }
            var roleName = role.Name;
            var result= await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["message"] = $"{roleName} role deleted successfully";
                return RedirectToPage("roles");
            }
            foreach (var error in result.Errors)
            {
                TempData["message"] = $"{error.Description} occured";
                return RedirectToPage("roles");
            }
            return RedirectToPage("roles");
        }
    }
}
