using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ResultChecker.Models;
using ResultChecker.Services;

namespace ResultChecker.Pages.Admin
{
    public class UploadResultModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UploadResultModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public IEnumerable<ResultUpload> MyResults { get; set; }
        [BindProperty]
        public ResultUpload ResultUpload { get; set; }
        [BindProperty]
        public string ConfirmResultUpload { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SessionList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> SemesterList { get; set; }
        public async Task OnGet()
        {
            SemesterList.Insert(0, new SelectListItem() { Text = "----Select Semester----", Value = "0" });
            SemesterList.Insert(1, new SelectListItem() { Text = "First", Value = "First" });
            SemesterList.Insert(2, new SelectListItem() { Text = "Second", Value = "Second" });
            SessionList = await dbContext.SessionSemesters.Select(x => new SelectListItem() { Text = x.Session, Value = x.Session }).Distinct().OrderByDescending(x => x.Value).ToListAsync();
            SessionList.Insert(0, new SelectListItem() { Text = "----Select Session----", Value = string.Empty });
            //TempData["upload"] = null;
            if (TempData["upload"]!=null)
            {
                List<ResultUpload> results = new List<ResultUpload>();
                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(TempData["upload"].ToString());

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        results.Add(new ResultUpload
                        {
                            MatNo = row.Split(',')[0],
                            CourseCode = row.Split(',')[1],
                            Score = Convert.ToInt32(row.Split(',')[2]),
                            Unit = Convert.ToInt32(row.Split(',')[3]),
                            Points = Convert.ToInt32(row.Split(',')[4]),
                            Grade = row.Split(',')[5],
                            GPA = row.Split(',')[6]
                        });
                    }
                }
                MyResults = results.ToList();
            }
           
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            string filePath = string.Empty;
            if (Upload != null)
            {
                string path = Path.Combine(webHostEnvironment.WebRootPath, "/result-upload/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
               

                string appendDate = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                var file = Path.Combine(webHostEnvironment.WebRootPath, "result-upload", appendDate + "_" + Upload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }

                TempData["upload"] = file;
                
            }
            return RedirectToPage("UploadResult");
        }
        public IActionResult OnPostConfirmUpload()
        {
            string UploadIdDate = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            List<ResultUpload> uploads = new List<ResultUpload>();
            List<ResultUpload> facebookFriends = JsonConvert.DeserializeObject<List<ResultUpload>>(ConfirmResultUpload);
            foreach (ResultUpload data in facebookFriends)
            {
                ResultUpload arr = new ResultUpload { MatNo = data.MatNo, CourseCode = data.CourseCode, Score = data.Score , Unit = data.Unit, Points = data.Points, Grade = data.Grade, GPA = data.GPA, UploadId =UploadIdDate, Session=ResultUpload.Session, Semester=ResultUpload.Semester };
                uploads.Add(arr);
            }
            dbContext.ResultUploads.AddRange(uploads);
            dbContext.SaveChanges();
            TempData["message"] = "Result uploaded successfully";
            return RedirectToPage("submittedResult");
        }
    }
   
}
