using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResultChecker.Models;
using ResultChecker.Services;

namespace ResultChecker.Pages.Admin
{
    public class EditCourseModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public EditCourseModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty(SupportsGet = true)]
        public Courses Courses { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SessionList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> ClassList { get; set; }
        public IActionResult OnGet()
        {
            var data = dbContext.Courses.FirstOrDefault(x => x.Id == Id);
            if (data == null)
            {
                TempData["message"] = $"record not found";
                return RedirectToPage("Courses");
            }
            SessionList = dbContext.SessionSemesters.Select(x => new SelectListItem { Text = x.Session, Value = x.Session.ToString() }).Distinct().ToList();
            SessionList.Insert(0, new SelectListItem("Select Semester", ""));
            //ClassList = dbContext..Select(x => new SelectListItem { Text = x.Name, Value = x.Name.ToString() }).Distinct().ToList();
            //ClassList.Insert(0, new SelectListItem("Select Class", ""));
            Courses.Id = data.Id;
            Courses.CourseCode = data.CourseCode;
            Courses.Semester = data.Semester;
            Courses.CourseUnit = data.CourseUnit;
            Courses.CourseTitle = data.CourseTitle;
            Courses.IsActive = data.IsActive;
            Courses.SubmittedBy = data.SubmittedBy;
            Courses.DateSubmitted = data.DateSubmitted.Date;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var data = dbContext.Courses.FirstOrDefault(x => x.Id == Courses.Id);
                if (data == null)
                {
                    TempData["message"] = $"record not found";
                    return RedirectToPage("courses");
                }
                data.CourseCode = Courses.CourseCode;
                data.Semester = Courses.Semester;
                data.CourseUnit = Courses.CourseUnit;
                data.CourseTitle = Courses.CourseTitle;
                data.SubmittedBy = User.Identity.Name.ToString();
                data.IsActive = Courses.IsActive;
                data.DateSubmitted = DateTime.Now.Date;
                dbContext.Courses.Update(data);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["message"] = $"{Courses.CourseCode} updated successfully";
                    return RedirectToPage("courses");
                }
                ModelState.AddModelError(string.Empty, "error, try again");
                return Page();
            }
            return Page();
        }
    }
}
