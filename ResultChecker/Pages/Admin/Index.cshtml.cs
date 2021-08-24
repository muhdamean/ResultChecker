using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResultChecker.Models;

namespace ResultChecker.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public IndexModel(SignInManager<ApplicationUser> signInManager)
        {
            
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("index");
        }
    }
}
