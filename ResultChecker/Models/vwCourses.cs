using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Models
{
    [NotMapped]
    public class vwCourses
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public string CourseUnit { get; set; }
        public string Year { get; set; }
        public string SubmittedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateSubmitted { get; set; }
        public bool IsActive { get; set; }
       
        public string Session { get; set; }
       
        public string Semester { get; set; }

        //migrationBuilder.Sql("CREATE VIEW vwSubjects as select s.*, st.Session, st.Term from Subjects s join SessionTerms st on s.SessionTermId = st.Id");
    }
}
