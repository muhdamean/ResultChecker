using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Models
{
    public class ResultUpload
    {
        public long Id { get; set; }
        public string MatNo { get; set; }
        public string CourseCode { get; set; }
        public int Score { get; set; }
        public string UploadId { get; set; }
        public string Session { get; set; }
        public string Semester { get; set; }
        public int Unit { get; set; }
        public int Points { get; set; }
        public string Grade { get; set; }
        public string GPA { get; set; }

        //public IFormFile CsvUpload { get; set; }
    }
}
