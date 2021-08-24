using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ResultChecker.Services;

namespace ResultChecker.Pages.Admin
{
    public class CreateRoleModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger<CreateRoleModel> logger;
        private readonly RoleManager<IdentityRole> roleManager;

        public CreateRoleModel(AppDbContext dbContext, ILogger<CreateRoleModel> logger, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.roleManager = roleManager;
        }
        [BindProperty]
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (Id != null)
            {
                var role = await roleManager.FindByIdAsync(Id);
                if (role == null)
                {
                    TempData["message"] = $"Role {Id} not found";
                    return RedirectToPage("roles");
                }
                Id = role.Id;
                RoleName = role.Name;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(Id);
                if (role != null)
                {
                    role.Name = RoleName;
                    IdentityResult identityResult = await roleManager.UpdateAsync(role);
                    if (identityResult.Succeeded)
                    {
                        TempData["message"] = $"{RoleName} role is updated successfully";
                        return RedirectToPage("roles");
                    }
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }

                IdentityRole identityRole = new IdentityRole
                {
                    Name = RoleName.Trim()
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    TempData["message"] = $"{RoleName} role is created successfully";
                    return RedirectToPage("roles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            return Page();
        }
    }
}
