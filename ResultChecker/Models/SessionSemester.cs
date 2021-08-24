using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Models
{
    public class SessionSemester
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="Maximum character 50")]
        public string Session { get; set; }
       // [Required]
        [MaxLength(50, ErrorMessage = "Maximum character 50")]
       //public string Semester { get; set; }
        public string SubmittedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
