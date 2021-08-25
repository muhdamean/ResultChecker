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
    public class MyResultModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext dbContext;

        public MyResultModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
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
        public async Task OnGet()
        {
            SemesterList.Insert(0, new SelectListItem() { Text = "----Select Semester----", Value = "0" });
            SemesterList.Insert(1, new SelectListItem() { Text = "First", Value = "First" });
            SemesterList.Insert(2, new SelectListItem() { Text = "Second", Value = "Second" });
            SessionList =  dbContext.SessionSemesters.Select(x => new SelectListItem() { Text = x.Session, Value = x.Session }).Distinct().OrderByDescending(x => x.Value).ToList();
            SessionList.Insert(0, new SelectListItem() { Text = "----Select Session----", Value = string.Empty });

            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {

            }
            if (TempData["result"]!=null)
            {
                Results = dbContext.vwUploadedResults.FromSqlRaw("Select * from vwresultupload").ToList().Where(x => x.MatNo == user.StaffID.Trim() && x.Semester == TempData["semester"].ToString() && x.Session == TempData["session"].ToString());
               // TempData["message"] = TempData["semester"].ToString() + " " + TempData["session"].ToString();
            }
            
        }
        public async Task<IActionResult> OnPost()
        {
            var user =await userManager.FindByEmailAsync(User.Identity.Name);
            if (user==null)
            {

            }
            Results = dbContext.vwUploadedResults.FromSqlRaw("Select * from vwresultupload").ToList().Where(x => x.MatNo == user.StaffID.Trim() && x.Semester == vwUploadedResults.Semester && x.Session == vwUploadedResults.Session);
            TempData["result"] = "result check successful";
            TempData["session"] = vwUploadedResults.Session;
            TempData["semester"] = vwUploadedResults.Semester;
            return RedirectToPage("myResult");
        }
    }
}
