using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Models
{
    public class CourseLecturer
    {
        public int Id { get; set; }
        public string Course { get; set; }
        [Display(Name ="Lecturer Name")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [Display(Name = "Session")]
        public string Session { get; set; }
        //[ForeignKey("SessionSemesterId")]
        //public SessionSemester SessionSemester { get; set; }
        public string SubmittedBy { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        [NotMapped]
        [Display(Name = "Lecturer Name")]
        public string StaffName { get; set; }
        //[NotMapped]
        //[Display(Name = "Session")]
        //public string Session { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string Phone { get; set; }
    }

}
