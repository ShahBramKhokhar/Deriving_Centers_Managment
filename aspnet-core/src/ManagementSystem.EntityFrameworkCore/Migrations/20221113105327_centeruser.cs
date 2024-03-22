using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    public partial class centeruser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Centres_UserId",
                table: "Centres");

            migrationBuilder.CreateIndex(
                name: "IX_Centres_UserId",
                table: "Centres",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Centres_UserId",
                table: "Centres");

            migrationBuilder.CreateIndex(
                name: "IX_Centres_UserId",
                table: "Centres",
                column: "UserId");
        }
    }
}
