using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    public partial class tacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "teachers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_UserId",
                table: "teachers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_AbpUsers_UserId",
                table: "teachers",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teachers_AbpUsers_UserId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_teachers_UserId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "teachers");
        }
    }
}
