using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Models
{
    public class CourseRegDetails
    {
        public long Id { get; set; }
        public long CourseRegId { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseRegId")]
        public CourseReg CourseReg { get; set; }
        [ForeignKey("CourseId")]
        public Courses Courses { get; set; }
    }
}
