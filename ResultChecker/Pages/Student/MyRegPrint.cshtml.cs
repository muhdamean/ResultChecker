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

namespace ResultChecker.Pages.Student
{
    public class MyRegPrintModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        public MyRegPrintModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public CourseReg MyCourses { get; set; }
        [BindProperty]
        public IEnumerable<vwCourses> vwCourses { get; set; }
        [BindProperty]
        public ApplicationUser applicationUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Session { get; set; } 
        [BindProperty(SupportsGet = true)]
        public string Semester { get; set; }
        public async Task<IActionResult> OnGet()
        {
            //SemesterList.Insert(0, new SelectListItem() { Text = "----Select Semester----", Value = "0" });
            //SemesterList.Insert(1, new SelectListItem() { Text = "First", Value = "First" });
            //SemesterList.Insert(2, new SelectListItem() { Text = "Second", Value = "Second" });
            //SessionList = dbContext.SessionSemesters.Select(x => new SelectListItem() { Text = x.Session, Value = x.Session }).Distinct().OrderByDescending(x => x.Value).ToList();
            //SessionList.Insert(0, new SelectListItem() { Text = "----Select Session----", Value = string.Empty });

            applicationUser =await userManager.GetUserAsync(User);

            var query = dbContext.CourseRegs
            .Join(
                dbContext.Courses,
                reg => reg.CourseCode,
                c => c.CourseCode,
                (reg, c) => new vwCourses
                {
                    Id = reg.Id,
                    CourseCode = reg.CourseCode,
                    CourseUnit = c.CourseUnit,
                    CourseTitle = c.CourseTitle,
                    Semester = reg.Semester,
                    Session = reg.Session,
                    Year = reg.Year,
                    SubmittedBy = reg.SubmittedBy,
                    
                }
            ).ToList().Where(x => x.SubmittedBy == User.Identity.Name.ToString() && x.Session == Session && x.Semester == Semester);

            vwCourses = query;
            return Page();
        }
    }
}
