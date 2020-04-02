namespace ITSkills.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedAboutInfoInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutInfo",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutInfo",
                table: "AspNetUsers");
        }
    }
}
