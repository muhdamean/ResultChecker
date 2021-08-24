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
    public class AddCourseModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AddCourseModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public Courses Courses { get; set; }
        //[BindProperty(SupportsGet = true)]
        //public List<SelectListItem> SessionList { get; set; }
        //[BindProperty(SupportsGet = true)]
        //public List<SelectListItem> ClassList { get; set; }

        public void OnGet()
        {
            //SessionList = dbContext.SessionSemesters.Select(x => new SelectListItem { Text = x.Semester, Value = x.Semester.ToString() }).Distinct().ToList();
            //SessionList.Insert(0, new SelectListItem("Select Session", ""));
            //ClassList = dbContext.Courses.Select(x => new SelectListItem { Text = x.CourseCode, Value = x.CourseCode.ToString() }).Distinct().ToList();
            //ClassList.Insert(0, new SelectListItem("Select Semester", ""));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Courses newCourse = new Courses
                {
                    CourseCode = Courses.CourseCode.Trim(),
                    Semester = Courses.Semester,
                    CourseTitle = Courses.CourseTitle,
                    IsActive = true,
                    SubmittedBy = User.Identity.Name.ToString(),
                    CourseUnit = Courses.CourseUnit,
                    DateSubmitted = DateTime.Now.Date
                };
                await dbContext.Courses.AddAsync(newCourse);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["message"] = $"{Courses.CourseCode} for {Courses.Semester} created successfully";
                    return RedirectToPage("courses");
                }
                Exception ex = new Exception();
                ModelState.AddModelError(string.Empty, "error, try again");
                return Page();
            }
            return Page();
        }
    }
}
