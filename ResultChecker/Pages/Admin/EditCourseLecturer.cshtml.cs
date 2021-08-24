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
    public class EditCourseLecturerModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public EditCourseLecturerModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty(SupportsGet = true)]
        public CourseLecturer CourseLecturer { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SessionList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> ClassList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> StaffList { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var data = await dbContext.CourseLecturers.FirstOrDefaultAsync(x => x.Id == Id);
            if (data == null)
            {
                TempData["message"] = $"record not found";
                return RedirectToPage("courselecturers");
            }
            StaffList = userManager.Users.AsNoTracking().Select(x => new SelectListItem { Text = x.FullName, Value = x.Id.ToString() }).Distinct().ToList();
            StaffList.Insert(0, new SelectListItem("Select Lecturer", ""));
            SessionList = dbContext.SessionSemesters.AsNoTracking().Select(x => new SelectListItem { Text = x.Session, Value = x.Id.ToString() }).Distinct().ToList();
            SessionList.Insert(0, new SelectListItem("Select Semester", ""));
            ClassList = dbContext.Courses.AsNoTracking().Select(x => new SelectListItem { Text = x.CourseCode + " - " + x.CourseTitle, Value = x.CourseCode }).Distinct().ToList();
            ClassList.Insert(0, new SelectListItem("Select Course", ""));

            CourseLecturer.Id = data.Id;
            CourseLecturer.UserId = data.UserId;
            CourseLecturer.Course = data.Course;
            CourseLecturer.Session = data.Session;
            CourseLecturer.SubmittedBy = data.SubmittedBy;
            CourseLecturer.DateCreated = data.DateCreated.Date;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var data = await dbContext.CourseLecturers.FindAsync(Id);
                if (data == null)
                {
                    TempData["message"] = $"Record not found";
                    return RedirectToPage("courselecturers");
                }
                data.UserId = CourseLecturer.UserId.Trim();
                data.Course = CourseLecturer.Course;
                data.Session = CourseLecturer.Session;
                data.SubmittedBy = User.Identity.Name.ToString();
                data.DateCreated = DateTime.Now.Date;
                await dbContext.CourseLecturers.AddAsync(data);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["message"] = $"{CourseLecturer.Course} lecturer updated successfully";
                    return RedirectToPage("courselecturers");
                }
                Exception ex = new Exception();
                ModelState.AddModelError(string.Empty, "error, try again");
                return Page();
            }
            return Page();
        }
    }
}
