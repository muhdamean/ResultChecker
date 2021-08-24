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
    public class CoursesModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public IEnumerable<Courses> Courses { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public void OnGet()
        {
            Courses = dbContext.Courses.ToList();
            //Subjects = (from s in dbContext.Subjects
            //        join st in dbContext.SessionTerms
            //        on s.SessionTermId equals st.Id
            //        join c in dbContext.Classes
            //        on s.ClassId equals c.Id
            //        select new Subject
            //        {
            //            Id = s.Id,
            //            ClassName = c.Name,
            //            Session =st.Session,
            //            Term=st.Term,
            //            IsActive = s.IsActive,
            //            SubmittedBy = s.SubmittedBy,
            //            DateSubmitted = s.DateSubmitted
            //        }).ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var check = dbContext.Courses.FirstOrDefault(x => x.Id == Id);
            if (check == null)
            {
                TempData["message"] = $"record not found";
                return RedirectToPage("courses");
            }
            var SubjectName = check.CourseCode;
            dbContext.Courses.Remove(check);
            var result = await dbContext.SaveChangesAsync();
            if (result > 0)
            {
                TempData["message"] = $"{SubjectName} course deleted successfully";
                return RedirectToPage("courses");
            }
            TempData["message"] = $"error, try again";
            return RedirectToPage("courses");
        }
    }
}
