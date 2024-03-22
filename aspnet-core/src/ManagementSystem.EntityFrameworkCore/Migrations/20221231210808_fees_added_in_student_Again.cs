﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    public partial class fees_added_in_student_Again : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalFees",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalFees",
                table: "students");
        }
    }
}
