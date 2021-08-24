using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResultChecker.Models;
using ResultChecker.Services;

namespace ResultChecker.Pages.Student
{
    public class AddStudentModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
       

        public AddStudentModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
           
        }
        [BindProperty]
        public ApplicationUser ApplicationUsers { get; set; }
        [BindProperty]
        public MailRequest mailRequest { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> DeptList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> GenderList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> RoleList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> StateList { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> LgaList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //gender list
            GenderList.Insert(0, new SelectListItem() { Text = "----Select Gender----", Value = "0" });
            GenderList.Insert(1, new SelectListItem() { Text = "Male", Value = "Male" });
            GenderList.Insert(2, new SelectListItem() { Text = "Female", Value = "Female" });
            //GenderList.Insert(3, new SelectListItem() { Text = "Other", Value = "Other" });
            //category list
            //RoleList.Insert(0, new SelectListItem() { Text = "----Select Role ----", Value = "0" });
            //RoleList.Insert(1, new SelectListItem() { Text = "HoD", Value = "HoD" });
            //RoleList.Insert(2, new SelectListItem() { Text = "Examiner", Value = "Examiner" });
            //RoleList.Insert(3, new SelectListItem() { Text = "Lecturer", Value = "Lecturer" });
            //RoleList.Insert(4, new SelectListItem() { Text = "Administration", Value = "Administration" });
            //RoleList.Insert(5, new SelectListItem() { Text = "Student", Value = "Student" });
            //department list
            //DeptList = await dbContext.Departments.Select(x => new SelectListItem() { Text = x.Name, Value = x.Name }).Distinct().ToListAsync();
            //DeptList.Insert(0, new SelectListItem() { Text = "----Select Department----", Value = string.Empty });
            ////state list
            StateList = await dbContext.StateLgas.Select(x => new SelectListItem() { Text = x.State, Value = x.State }).Distinct().OrderBy(x => x.Text).ToListAsync();
            StateList.Insert(0, new SelectListItem() { Text = "----Select State----", Value = string.Empty });
            //lga list
            //LgaList = await _db.StateLgas.Select(x =>  x.Lga ).Distinct().ToListAsync();
            LgaList.Insert(0, new SelectListItem() { Text = "----Select LGA----", Value = string.Empty });
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                string uniqueFilename = null, filePath = null;
                if (ApplicationUsers.PassportPhoto != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(ApplicationUsers.PassportPhoto.FileName);
                    string extension = Path.GetExtension(ApplicationUsers.PassportPhoto.FileName);
                    uniqueFilename = filename + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + extension;
                    filePath = Path.Combine(webHostEnvironment.WebRootPath + "/images/", uniqueFilename);
                }
                ApplicationUser user = new ApplicationUser
                {
                    StaffID = ApplicationUsers.StaffID,
                    Email = ApplicationUsers.Email,
                    UserName = ApplicationUsers.Email,
                    FirstName = ApplicationUsers.FirstName,
                    MiddleName = ApplicationUsers.MiddleName,
                    LastName = ApplicationUsers.LastName,
                    PhoneNumber = ApplicationUsers.PhoneNumber,
                    Gender = ApplicationUsers.Gender,
                    Department = ApplicationUsers.Department,
                    Qualification = ApplicationUsers.Qualification,
                    Address = ApplicationUsers.Address,
                    State = ApplicationUsers.State,
                    LGA = ApplicationUsers.LGA,
                    NOKName = ApplicationUsers.NOKName,
                    NOKPhone = ApplicationUsers.NOKPhone,
                    NOKAddress = ApplicationUsers.NOKAddress,
                    IsStaff = false,
                    //CompleteReg = true,
                    Passport = uniqueFilename,
                    DOB = ApplicationUsers.DOB.Date,
                    Role ="Student",
                    DateSubmitted = DateTime.Now.Date,
                };

                var result = await userManager.CreateAsync(user, ApplicationUsers.PhoneNumber);
                if (result.Succeeded)
                {
                    if (ApplicationUsers.PassportPhoto != null)
                    {
                        //save passport
                        await ApplicationUsers.PassportPhoto.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    }
                    //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var confirmationLink = Url.Page("/account/confirmemail", null, new { tid = user.Id, token = token }, Request.Scheme);
                    ////send mail
                    //mailRequest.Subject = "Email Confirmation";
                    //mailRequest.ToEmail = ApplicationUsers.Email;
                    //mailRequest.Body = $@"<p>Hi {ApplicationUsers.FullName}, your registration with Staff ID {ApplicationUsers.StaffID} is recieved.</p>
                    //                        <p>Kindly click on the link below to confirm your email <br/></p>
                    //                     <a href={confirmationLink}>Confirm your email</a>.<br/><br/>
                    //                    Please kindly disregard this email if you did not initiate the above.  Thanks!<br/>";
                    //await mailService.SendEmailAsync(mailRequest);
                    //log to local folder
                    //logger.Log(LogLevel.Warning, confirmationLink);
                    TempData["message"] = $"Created successfully. ";
                    return RedirectToPage("index");
                }
                Exception ex = new Exception();
                ModelState.AddModelError(string.Empty, "error, try again " + ex.Message);
                return Page();
            }
            return Page();
        }
        public async Task<JsonResult> OnGetLga(string state)
        {
            var lgaList = new List<SelectListItem>();
            lgaList = await dbContext.StateLgas.Where(x => x.State == state).Select(x => new SelectListItem() { Text = x.LGA, Value = x.LGA }).Distinct().ToListAsync(); //call repository
            return new JsonResult(lgaList);
        }
    }
}
