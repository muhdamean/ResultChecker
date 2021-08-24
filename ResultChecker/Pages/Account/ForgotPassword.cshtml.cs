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
    public class ForgotPasswordModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
 

        public ForgotPasswordModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public string Email { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(Email);
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink = Url.Action("resetpassword", "account", new { email = Email, token = token }, Request.Scheme);
                    //logger.Log(LogLevel.Warning, passwordResetLink); //send mail here
                    return RedirectToPage("sendlinkconfirmation");
                }
                else if (user == null)
                {
                    //ViewBag.ErrorTitle = "Invalid Email";
                    TempData["message"] = $"Email {Email} not found";
                    return RedirectToPage("notfound");
                }
                return Page();
            }
            return Page();
        }
    }
}
