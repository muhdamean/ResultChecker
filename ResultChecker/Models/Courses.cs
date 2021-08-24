using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Models
{
    public class Courses
    {        
        public int Id { get; set; }
        [Required]
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; }
        [Display(Name = "Course Title")]
        public string CourseTitle { get; set; }
        [Display(Name = "Course Unit")]
        public string CourseUnit { get; set; }
        [Required]
        public string Semester { get; set; }
        public string Session { get; set; }
        [Display(Name = "Core Course")]
        public string IsCore { get; set; }
        public string SubmittedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateSubmitted { get; set; }
        public bool IsActive { get; set; }
    }
}
