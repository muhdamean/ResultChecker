using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResultChecker.Models;
using ResultChecker.Pages.Admin;
using ResultChecker.Services;

namespace ResultChecker.Pages.Admin
{
    public class EditStaffModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<AddStaffModel> logger;
        private readonly IMailService mailService;

        public EditStaffModel(AppDbContext dbContext, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ILogger<AddStaffModel> logger, IMailService mailService)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
            this.mailService = mailService;
        }
        [BindProperty(SupportsGet = true)]
        public ApplicationUser ApplicationUsers { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string OldPhoto { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<string> ClaimsList { get; set; }
        [BindProperty(SupportsGet = true)]
        public IList<string> RolesList { get; set; }
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
        public List<SelectListItem> LgaList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var data = await userManager.FindByIdAsync(Id);
            if (data == null)
            {
                TempData["message"] = $"User not found";
                return RedirectToPage("../admin/staff");
            }
            var userClaims = await userManager.GetClaimsAsync(data);
            var userRoles = await userManager.GetRolesAsync(data);
            //gender list
            GenderList.Insert(0, new SelectListItem() { Text = "----Select Gender----", Value = "0" });
            GenderList.Insert(1, new SelectListItem() { Text = "Male", Value = "Male" });
            GenderList.Insert(2, new SelectListItem() { Text = "Female", Value = "Female" });
            GenderList.Insert(3, new SelectListItem() { Text = "Other", Value = "Other" });
            //category list
            RoleList.Insert(0, new SelectListItem() { Text = "----Select Role ----", Value = "0" });
            RoleList.Insert(1, new SelectListItem() { Text = "HoD", Value = "HoD" });
            RoleList.Insert(2, new SelectListItem() { Text = "Examiner", Value = "Examiner" });
            RoleList.Insert(3, new SelectListItem() { Text = "Lecturer", Value = "Lecturer" });
            RoleList.Insert(4, new SelectListItem() { Text = "Administration", Value = "Administration" });
            RoleList.Insert(5, new SelectListItem() { Text = "Others", Value = "Others" });
            //department list
            //DeptList = await dbContext.Departments.Select(x => new SelectListItem() { Text = x.Name, Value = x.Name }).Distinct().ToListAsync();
            //DeptList.Insert(0, new SelectListItem() { Text = "----Select Department----", Value = string.Empty });
            //state list
            StateList = await dbContext.StateLgas.Select(x => new SelectListItem() { Text = x.State, Value = x.State }).Distinct().ToListAsync();
            StateList.Insert(0, new SelectListItem() { Text = "----Select State----", Value = string.Empty });
            //lga list
            //LgaList = await _db.StateLgas.Select(x =>  x.Lga ).Distinct().ToListAsync();
            LgaList.Insert(0, new SelectListItem() { Text = "----Select LGA----", Value = string.Empty });


            ApplicationUsers.StaffID = data.StaffID;
            ApplicationUsers.Email = data.Email;
            ApplicationUsers.Email = data.UserName;
            ApplicationUsers.FirstName = data.FirstName;
            ApplicationUsers.MiddleName = data.MiddleName;
            ApplicationUsers.LastName = data.LastName;
            ApplicationUsers.PhoneNumber = data.PhoneNumber;
            ApplicationUsers.Gender = data.Gender;
            ApplicationUsers.Department = data.Department;
            ApplicationUsers.Qualification = data.Qualification;
            
            ApplicationUsers.Address = data.Address;
            ApplicationUsers.State = data.State;
            ApplicationUsers.LGA = data.LGA;
            
            ApplicationUsers.NOKName = data.NOKName;
            ApplicationUsers.NOKPhone = data.NOKPhone;
            ApplicationUsers.NOKAddress = data.NOKAddress;
            ApplicationUsers.IsStaff = data.IsStaff;
            
            ApplicationUsers.Passport = data.Passport;
            ApplicationUsers.DOB = data.DOB.Date;
            ApplicationUsers.Role = data.Role;
            OldPhoto = data.Passport;

            ClaimsList = userClaims.Select(c => c.Type + " : " + c.Value).ToList();
            RolesList = userRoles;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(ApplicationUsers.Id);
                if (user == null)
                {
                    TempData["message"] = $"User not found";
                    return RedirectToPage("../admin/staff");
                }
                string uniqueFilename = null, filePath = null;
                if (ApplicationUsers.Passport != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(ApplicationUsers.PassportPhoto.FileName);
                    string extension = Path.GetExtension(ApplicationUsers.PassportPhoto.FileName);
                    uniqueFilename = filename + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + extension;
                    filePath = Path.Combine(webHostEnvironment.WebRootPath + "/images/", uniqueFilename);
                }

                user.StaffID = ApplicationUsers.StaffID;
                user.Email = ApplicationUsers.Email;
                user.UserName = ApplicationUsers.Email;
                user.FirstName = ApplicationUsers.FirstName;
                user.MiddleName = ApplicationUsers.MiddleName;
                user.LastName = ApplicationUsers.LastName;
                user.PhoneNumber = ApplicationUsers.PhoneNumber;
                user.Gender = ApplicationUsers.Gender;
                user.Department = ApplicationUsers.Department;
                user.Qualification = ApplicationUsers.Qualification;
              
                user.Address = ApplicationUsers.Address;
                user.State = ApplicationUsers.State;
                user.LGA = ApplicationUsers.LGA;
                
                user.NOKName = ApplicationUsers.NOKName;
                user.NOKPhone = ApplicationUsers.NOKPhone;
                user.NOKAddress = ApplicationUsers.NOKAddress;
                user.IsStaff = true;
                
                user.Passport = uniqueFilename ?? OldPhoto.Trim();
                user.DOB = ApplicationUsers.DOB.Date;
                user.Role = ApplicationUsers.Role;
                user.DateSubmitted = DateTime.Now.Date;

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (ApplicationUsers.PassportPhoto != null)
                    {
                        //save passport
                        await ApplicationUsers.PassportPhoto.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    }
                    //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var confirmationLink = Url.Action("confirmemail", "account", new { userId = user.Id, token = token }, Request.Scheme);
                    ////send mail
                    //mailRequest.Subject = "Email Confirmation";
                    //mailRequest.ToEmail = ApplicationUsers.Email;
                    //mailRequest.Body = $@"<p>Hi {ApplicationUsers.FullName}, your registration is recieved.</p>
                    //                        <p>Kindly click on the link below to confirm your email <br/></p>
                    //                     {confirmationLink}.<br/><br/>
                    //                    Please kindly disregard this email if you did not initiate the above.  Thanks!<br/>";
                    //await mailService.SendEmailAsync(mailRequest);
                    //log to local folder
                    //logger.Log(LogLevel.Warning, confirmationLink);
                    TempData["message"] = $"{ApplicationUsers.Email} updated successfully";
                    return RedirectToPage("staffdetail");
                }
                Exception ex = new Exception();
                ModelState.AddModelError(string.Empty, "error, try again");
                return Page();
            }

            return Page();
        }
    }
}
