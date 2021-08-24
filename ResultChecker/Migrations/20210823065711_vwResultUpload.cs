using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultChecker.Migrations
{
    public partial class vwResultUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE VIEW vwresultupload AS SELECT r.*, c.CourseTitle, c.CourseUnit  FROM ResultUploads r JOIN Courses c ON r.CourseCode = c.CourseCode;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vwresultupload;");
        }
    }
}
