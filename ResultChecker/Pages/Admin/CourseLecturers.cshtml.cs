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
    public class CourseLecturersModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public CourseLecturersModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public IEnumerable<CourseLecturer> CourseLecturers { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public void OnGet()
        {
            CourseLecturers = dbContext.CourseLecturers.ToList();

            CourseLecturers = (from c in dbContext.CourseLecturers
                               join u in userManager.Users on c.UserId equals u.Id
                               select new CourseLecturer
                               {
                                   Id = c.Id,
                                   Course = c.Course,
                                   Session = c.Session,
                                   StaffName = u.FullName,
                                   Phone=u.PhoneNumber,
                                   Email=u.Email,
                                   //Term = st.Term,
                                   IsActive = c.IsActive,
                                   SubmittedBy = c.SubmittedBy,
                                   DateCreated = c.DateCreated
                               }).ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var check = dbContext.CourseLecturers.FirstOrDefault(x => x.Id == Id);
            if (check == null)
            {
                TempData["message"] = $"record not found";
                return RedirectToPage("subjectTeachers");
            }
            dbContext.CourseLecturers.Remove(check);
            var result = await dbContext.SaveChangesAsync();
            if (result > 0)
            {
                TempData["message"] = $"Subject Teacher deleted successfully";
                return RedirectToPage("subjectTeachers");
            }
            TempData["message"] = $"error, try again";
            return RedirectToPage("subjectTeachers");
        }
    }
}
