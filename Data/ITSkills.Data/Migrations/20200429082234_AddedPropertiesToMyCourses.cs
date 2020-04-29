using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITSkills.Data.Migrations
{
    public partial class AddedPropertiesToMyCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPayed",
                table: "MyCourses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentCode",
                table: "MyCourses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "MyCourses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "MyCourses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "MyCourses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPayed",
                table: "MyCourses");

            migrationBuilder.DropColumn(
                name: "PaymentCode",
                table: "MyCourses");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "MyCourses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MyCourses");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "MyCourses");
        }
    }
}
