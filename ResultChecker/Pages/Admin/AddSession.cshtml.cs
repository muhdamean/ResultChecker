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
    public class AddSessionModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AddSessionModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public SessionSemester sessionTerm { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                SessionSemester newSession = new SessionSemester
                {
                    Session = sessionTerm.Session.Trim(),
                    //Semester = sessionTerm.Semester,
                    IsActive = true,
                    SubmittedBy = User.Identity.Name.ToString(),
                    //DateEnd = sessionTerm.DateEnd,
                    //DateBegin = sessionTerm.DateBegin,
                    DateCreated = DateTime.Now.Date
                };
                await dbContext.SessionSemesters.AddAsync(newSession);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["message"] = $"{sessionTerm.Session} session created successfully";
                    return RedirectToPage("sessionsemesters");
                }
                Exception ex = new Exception();
                ModelState.AddModelError(string.Empty, "error, try again");
                return Page();
            }
            return Page();
        }
    }
}
