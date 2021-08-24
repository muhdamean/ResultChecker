using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Models
{
    public class CourseReg
    {
        public long Id { get; set; }
        [Display(Name ="Student Name")]
        public string UserId { get; set; }
        public string Session { get; set; }
        public string CourseCode { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string SubmittedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateSubmitted { get; set; }
        public bool IsApproved { get; set; }
       
    }
}
