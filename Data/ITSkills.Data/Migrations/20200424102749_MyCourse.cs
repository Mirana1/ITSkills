using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITSkills.Data.Migrations
{
    public partial class MyCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyCourseCourseId",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MyCourseUserId",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MyCourses",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCourses", x => new { x.UserId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_MyCourses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MyCourseUserId_MyCourseCourseId",
                table: "Courses",
                columns: new[] { "MyCourseUserId", "MyCourseCourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_MyCourses_IsDeleted",
                table: "MyCourses",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_MyCourses_MyCourseUserId_MyCourseCourseId",
                table: "Courses",
                columns: new[] { "MyCourseUserId", "MyCourseCourseId" },
                principalTable: "MyCourses",
                principalColumns: new[] { "UserId", "CourseId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_MyCourses_MyCourseUserId_MyCourseCourseId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "MyCourses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MyCourseUserId_MyCourseCourseId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MyCourseCourseId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MyCourseUserId",
                table: "Courses");
        }
    }
}
