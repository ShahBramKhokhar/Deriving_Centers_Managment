using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    public partial class studentsRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_Centres_CenterId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_CenterId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_CenterId",
                table: "students",
                column: "CenterId",
                unique: true,
                filter: "[CenterId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_students_Centres_CenterId",
                table: "students",
                column: "CenterId",
                principalTable: "Centres",
                principalColumn: "Id");
        }
    }
}
