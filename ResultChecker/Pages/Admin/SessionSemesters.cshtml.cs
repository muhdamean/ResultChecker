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
    public class SessionSemestersModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public SessionSemestersModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public IEnumerable<SessionSemester> SessionTerms { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public void OnGet()
        {
            SessionTerms = dbContext.SessionSemesters.ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var check = dbContext.SessionSemesters.FirstOrDefault(x => x.Id == Id);
            if (check == null)
            {
                TempData["message"] = $"record not found";
                return RedirectToPage("sessionterms");
            }
            var sessionTerm = check.Session;
            dbContext.SessionSemesters.Remove(check);
            var result = await dbContext.SaveChangesAsync();
            if (result > 0)
            {
                TempData["message"] = $"{sessionTerm} session deleted successfully";
                return RedirectToPage("sessionterms");
            }
            TempData["message"] = $"error, try again";
            return RedirectToPage("sessionterms");
        }
    }
}
