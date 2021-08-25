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

namespace ResultChecker.Pages.Admin
{
    public class AddCourseLecturersModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AddCourseLecturersModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public CourseLecturer CourseLecturers { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SessionList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> ClassList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> StaffList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SubjectList { get; set; }

        public void OnGet()
        {
            StaffList = userManager.Users.AsNoTracking().Where(x=>x.IsStaff==true).Select(x => new SelectListItem { Text = x.FullName, Value = x.Id.ToString() }).Distinct().ToList();
            StaffList.Insert(0, new SelectListItem("Select Lecturer", ""));
            SessionList = dbContext.SessionSemesters.AsNoTracking().Select(x => new SelectListItem { Text = x.Session , Value = x.Session.ToString() }).Distinct().ToList();
            SessionList.Insert(0, new SelectListItem("Select Session", ""));
            //ClassList = dbContext.Classes.AsNoTracking().Select(x => new SelectListItem { Text = x.Name + " - " + x.Description, Value = x.Id.ToString() }).Distinct().ToList();
            //ClassList.Insert(0, new SelectListItem("Select Class", ""));
            SubjectList = dbContext.Courses.AsNoTracking().Select(x => new SelectListItem { Text = x.CourseCode + " - " + x.CourseTitle, Value = x.CourseCode }).Distinct().ToList();
            SubjectList.Insert(0, new SelectListItem("Select Course", ""));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CourseLecturer newTeacher = new CourseLecturer
                {
                    UserId = CourseLecturers.UserId.Trim(),
                    Session = CourseLecturers.Session,
                    Course = CourseLecturers.Course,
                    IsActive = true,
                    SubmittedBy =User.Identity.Name,
                    DateCreated = DateTime.Now.Date
                };
                await dbContext.CourseLecturers.AddAsync(newTeacher);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["message"] = $"Course Lecturer added successfully";
                    return RedirectToPage("courseLecturers");
                }
                Exception ex = new Exception();
                ModelState.AddModelError(string.Empty, "error, try again");
                return Page();
            }
            return Page();
        }
    }
}
