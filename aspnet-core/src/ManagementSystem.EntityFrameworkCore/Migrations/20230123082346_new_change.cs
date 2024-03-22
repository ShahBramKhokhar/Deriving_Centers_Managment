using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    public partial class new_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_CenterId",
                table: "students",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_Centres_CenterId",
                table: "students",
                column: "CenterId",
                principalTable: "Centres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
