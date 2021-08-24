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
    public class SubmittedResultModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext dbContext;

        public SubmittedResultModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
        }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<vwUploadedResults> Results { get; set; }
        [BindProperty]
        public vwUploadedResults vwUploadedResults { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public ApplicationUser Usersss { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SessionList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SemesterList { get; set; }
        public void OnGet()
        {
            SemesterList.Insert(0, new SelectListItem() { Text = "----Select Semester----", Value = "0" });
            SemesterList.Insert(1, new SelectListItem() { Text = "First", Value = "First" });
            SemesterList.Insert(2, new SelectListItem() { Text = "Second", Value = "Second" });
            SessionList = dbContext.SessionSemesters.Select(x => new SelectListItem() { Text = x.Session, Value = x.Session }).Distinct().OrderByDescending(x => x.Value).ToList();
            SessionList.Insert(0, new SelectListItem() { Text = "----Select Session----", Value = string.Empty });
            Results = dbContext.vwUploadedResults.FromSqlRaw("Select * from vwresultupload").ToList().OrderByDescending(x => x.UploadId);
        }
        public IActionResult OnPost()
        {
            Results = dbContext.vwUploadedResults.FromSqlRaw("Select * from vwresultupload").ToList().OrderByDescending(x => x.UploadId);
            return Page();
        }
    }
}
