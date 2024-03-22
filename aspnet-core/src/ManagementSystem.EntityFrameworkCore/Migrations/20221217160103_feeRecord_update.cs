using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    public partial class feeRecord_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalFees",
                table: "feeRecords",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "RemainingFees",
                table: "feeRecords",
                newName: "Remaining");

            migrationBuilder.AddColumn<decimal>(
                name: "Paid",
                table: "feeRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "feeRecords");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "feeRecords",
                newName: "TotalFees");

            migrationBuilder.RenameColumn(
                name: "Remaining",
                table: "feeRecords",
                newName: "RemainingFees");
        }
    }
}
