using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResultChecker.Models;
using ResultChecker.Services;

namespace ResultChecker.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        [BindProperty(SupportsGet =true)]
        public string ReturnUrl { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public bool RememberMe { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            //begin set patient role here
            var users = userManager.Users.ToList().Where(x => x.IsStaff == false);
            if (users != null)
            {
                string role = "Student";
                foreach (var item in users)
                {
                    var userRole = await roleManager.FindByIdAsync(item.Id);
                    if (userRole == null)
                    {
                        await userManager.AddToRoleAsync(item, role);
                    }

                }
            }//end set patient role here

            //model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(Email);
                
                var result = await signInManager.PasswordSignInAsync(Email, Password, RememberMe, false);
                if (result.Succeeded)
                {
                    //var regStatus = await userManager.FindByEmailAsync(Email);
                    //if (string.IsNullOrEmpty(ReturnUrl))
                    //{
                            //specify admin or student redirect here
                            //if (regStatus.IsStaff == true)
                            //{
                                return RedirectToPage("../admin/index");
                           // }
                    //}
                    //else
                    //{
                    //    return Page();
                    //}
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return Page();
        }
    }
}
