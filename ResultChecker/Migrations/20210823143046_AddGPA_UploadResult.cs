using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultChecker.Migrations
{
    public partial class AddGPA_UploadResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "gpa",
                table: "resultuploads",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gpa",
                table: "resultuploads");
        }
    }
}
