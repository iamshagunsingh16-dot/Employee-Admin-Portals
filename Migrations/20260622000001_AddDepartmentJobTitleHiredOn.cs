using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Admin_Portal.Migrations
{
    public partial class AddDepartmentJobTitleHiredOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Employees",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Employees",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HiredOn",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Department", table: "Employees");
            migrationBuilder.DropColumn(name: "JobTitle", table: "Employees");
            migrationBuilder.DropColumn(name: "HiredOn", table: "Employees");
        }
    }
}
