using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    public partial class centre_tbl_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Centres_AbpUsers_UserId",
                table: "Centres");

            migrationBuilder.DropIndex(
                name: "IX_Centres_UserId",
                table: "Centres");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Centres",
                newName: "User");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Centres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Centres_User",
                table: "Centres",
                column: "User",
                unique: true,
                filter: "[User] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Centres_AbpUsers_User",
                table: "Centres",
                column: "User",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Centres_AbpUsers_User",
                table: "Centres");

            migrationBuilder.DropIndex(
                name: "IX_Centres_User",
                table: "Centres");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Centres");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "AbpUsers");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Centres",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Centres_UserId",
                table: "Centres",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Centres_AbpUsers_UserId",
                table: "Centres",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
