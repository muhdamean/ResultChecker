using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string StaffID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Maximum character 20")]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Qualification { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum character 20")]
        public string Religion { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character 50")]
        public string Tribe { get; set; }
        [Display(Name = "Hobby")]
        public string Hobbies { get; set; }
        public string Level { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum character 100")]
        public string State { get; set; }
        public string LGA { get; set; }
        [Display(Name = "NOK Name")]
        public string NOKName { get; set; }
        [Display(Name = "NOK Phone")]
        public string NOKPhone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "NOK Email")]
        public string NOKEmail { get; set; }
        [Display(Name = "NOK Address")]
        public string NOKAddress { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsStaff { get; set; }
        public string Passport { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateSubmitted { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        [Display(Name = "Name")]
        public string FullName
        {
            get{return FirstName + " " + MiddleName+" "+LastName;}
        }
        [NotMapped]
        [Display(Name = "Passport")]
        public IFormFile PassportPhoto { get; set; }
    }
}
