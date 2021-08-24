using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultChecker.Migrations
{
    public partial class addDepartment_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_courselecturers_sessionsemesters_sessionsemesterid",
                table: "courselecturers");

            migrationBuilder.DropIndex(
                name: "ix_courselecturers_sessionsemesterid",
                table: "courselecturers");

            migrationBuilder.DropColumn(
                name: "sessionsemesterid",
                table: "courselecturers");

            migrationBuilder.AddColumn<string>(
                name: "session",
                table: "courselecturers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "session",
                table: "courselecturers");

            migrationBuilder.DropColumn(
                name: "department",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "sessionsemesterid",
                table: "courselecturers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_courselecturers_sessionsemesterid",
                table: "courselecturers",
                column: "sessionsemesterid");

            migrationBuilder.AddForeignKey(
                name: "fk_courselecturers_sessionsemesters_sessionsemesterid",
                table: "courselecturers",
                column: "sessionsemesterid",
                principalTable: "sessionsemesters",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
