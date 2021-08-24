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
    public class EditSessionModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public EditSessionModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty(SupportsGet = true)]
        public SessionSemester SessionSemester { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public IActionResult OnGet()
        {
            var data = dbContext.SessionSemesters.FirstOrDefault(x => x.Id == Id);
            if (data == null)
            {
                TempData["message"] = $"record not found";
                return RedirectToPage("SessionSemesters");
            }
            SessionSemester.Id = data.Id;
            SessionSemester.Session = data.Session;
            //SessionSemester.Semester = data.Semester;
            SessionSemester.IsActive = data.IsActive;
            SessionSemester.SubmittedBy = data.SubmittedBy;
            SessionSemester.DateCreated = data.DateCreated.Date;
            //SessionSemester.DateEnd = data.DateEnd.Date;
            //SessionSemester.DateBegin = data.DateBegin.Date;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var data = dbContext.SessionSemesters.FirstOrDefault(x => x.Id == SessionSemester.Id);
                if (data == null)
                {
                    TempData["message"] = $"record not found";
                    return RedirectToPage("SessionSemesters");
                }
                data.Session = SessionSemester.Session;
                //data.Semester = SessionSemester.Semester;
                data.SubmittedBy = User.Identity.Name.ToString();
                data.IsActive = SessionSemester.IsActive;
                data.DateCreated = DateTime.Now.Date;
                //data.DateEnd = SessionSemester.DateEnd.Date;
                //data.DateBegin = SessionSemester.DateBegin.Date;
                dbContext.SessionSemesters.Update(data);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["message"] = $"{SessionSemester.Session} session updated successfully";
                    return RedirectToPage("SessionSemesters");
                }
                ModelState.AddModelError(string.Empty, "error, try again");
                return Page();
            }
            return Page();
        }
    }
}
