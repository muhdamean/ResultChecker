using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResultChecker.Models;
using ResultChecker.Services;

namespace ResultChecker.Pages.Admin
{
    public class StaffModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public StaffModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [BindProperty(SupportsGet =true)]
        public IEnumerable<ApplicationUser> Users { get; set; }
        public async Task<IActionResult> OnGet()
        {
            /* var confirmationLink = Url.Page("/account/confirmemail", pageHandler:null, values: new { tid = "123", token = "adjakdja" }, Request.Scheme );*/
            Users = await userManager.Users.Where(u => u.IsStaff == true).ToListAsync();

            //TempData["message"] = $@"<p> <p>Kindly click on the link below to confirm your email <br/></p> <a href=""{confirmationLink}"">Confirm your email</a>.<br/><br/> Please kindly disregard this email if you did not initiate the above.  Thanks!<br/>";
            //TempData["m"] = $"hey link here:" + confirmationLink;
            return Page();
        }
    }
}
