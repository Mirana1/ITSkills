using Microsoft.EntityFrameworkCore.Migrations;

namespace ITSkills.Data.Migrations
{
    public partial class AddedRequirementsAcquiredKnowledgeToCourseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcquiredKnowledge",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcquiredKnowledge",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "Courses");
        }
    }
}
