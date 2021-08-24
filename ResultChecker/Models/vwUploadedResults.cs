using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Models
{ 
    [NotMapped]
    public class vwUploadedResults
    {
        public string MatNo { get; set; }
        public string CourseCode { get; set; }
        public int Score { get; set; }
        public string CourseTitle { get; set; }
        public int Unit { get; set; }
        public int Points { get; set; }
        public string Grade { get; set; }
        public string Semester { get; set; }
        public string Session { get; set; }
        public string UploadId { get; set; }
        public string GPA { get; set; }

    }
}
