using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResultChecker.Models;
using ResultChecker.Services;

namespace ResultChecker.Pages.Student
{
    public class CourseRegistrationModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public CourseRegistrationModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public Courses Courses { get; set; }
        [BindProperty]
        public ApplicationUser Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> CourseList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SessionList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SemesterList { get; set; }
        [BindProperty]
        public CourseReg CourseRegs { get; set; }
        [BindProperty]
        public CourseRegDetails CourseRegDetails { get; set; }
        public async Task<IActionResult> OnGet()
        {
            SessionList = await dbContext.SessionSemesters.Select(x => new SelectListItem() { Text = x.Session, Value = x.Session }).Distinct().OrderByDescending(x => x.Value).ToListAsync();
            SessionList.Insert(0, new SelectListItem() { Text = "----Select Session----", Value = string.Empty });

            //course list
            CourseList = await dbContext.Courses.Select(x => new SelectListItem() { Text = x.CourseCode+ " - "+x.CourseUnit, Value = x.CourseCode }).Distinct().OrderByDescending(x => x.Value).ToListAsync();
            CourseList.Insert(0, new SelectListItem() { Text = "----Select Course----", Value = string.Empty });

            //semester list
            SemesterList.Insert(0, new SelectListItem() { Text = "----Select Semester----", Value = "0" });
            SemesterList.Insert(1, new SelectListItem() { Text = "First", Value = "First" });
            SemesterList.Insert(2, new SelectListItem() { Text = "Second", Value = "Second" });
            return Page();
        }
       public async Task<IActionResult> OnPost()
       {
           var check= await dbContext.CourseRegs.FirstOrDefaultAsync(x => x.CourseCode == CourseRegs.CourseCode && x.Session == CourseRegs.Session);
            if (check!=null)
            {
                TempData["message"] = "Course registered for this session already";
                return Page();
            }

            var newCourseReg = new CourseReg
            {
                Session = CourseRegs.Session,
                Semester = CourseRegs.Semester,
                CourseCode = CourseRegs.CourseCode,
                Year = CourseRegs.Year,
                SubmittedBy = User.Identity.Name
                
            };
            dbContext.CourseRegs.Add(newCourseReg);
           await dbContext.SaveChangesAsync();
            TempData["message"] = $"{CourseRegs.CourseCode} registered for {CourseRegs.Semester} semester {CourseRegs.Session} session";
           return RedirectToPage("myCourseReg");
       }
    }
}
