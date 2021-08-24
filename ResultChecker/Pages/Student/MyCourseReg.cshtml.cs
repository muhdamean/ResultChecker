using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class MyCourseRegModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        public MyCourseRegModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public IEnumerable<CourseReg> MyCourses { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SessionList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SemesterList { get; set; }
        [BindProperty]
        [Required]
        public string Session { get; set; }
        [BindProperty]
        [Required]
        public string Semester { get; set; }
        public void OnGet()
        {
            SemesterList.Insert(0, new SelectListItem() { Text = "----Select Semester----", Value = "0" });
            SemesterList.Insert(1, new SelectListItem() { Text = "First", Value = "First" });
            SemesterList.Insert(2, new SelectListItem() { Text = "Second", Value = "Second" });
            SessionList = dbContext.SessionSemesters.Select(x => new SelectListItem() { Text = x.Session, Value = x.Session }).Distinct().OrderByDescending(x => x.Value).ToList();
            SessionList.Insert(0, new SelectListItem() { Text = "----Select Session----", Value = string.Empty });
            var u = User.Identity.Name.ToString();

            MyCourses = dbContext.CourseRegs.Where(x=>x.SubmittedBy==User.Identity.Name.ToString()).ToList().OrderByDescending(x => x.DateSubmitted);
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("MyRegPrint", new { session = Session, semester = Semester });
        }
    }
}
