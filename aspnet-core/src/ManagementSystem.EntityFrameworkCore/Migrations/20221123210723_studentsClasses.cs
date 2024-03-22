using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    public partial class studentsClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classes_teachers_TeacherId",
                table: "classes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_classes",
                table: "classes");

            migrationBuilder.RenameTable(
                name: "classes",
                newName: "classArea");

            migrationBuilder.RenameIndex(
                name: "IX_classes_TeacherId",
                table: "classArea",
                newName: "IX_classArea_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_classArea",
                table: "classArea",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "studentsClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentsClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_studentsClasses_classArea_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_studentsClasses_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_studentsClasses_ClassId",
                table: "studentsClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_studentsClasses_StudentId",
                table: "studentsClasses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_classArea_teachers_TeacherId",
                table: "classArea",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classArea_teachers_TeacherId",
                table: "classArea");

            migrationBuilder.DropTable(
                name: "studentsClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_classArea",
                table: "classArea");

            migrationBuilder.RenameTable(
                name: "classArea",
                newName: "classes");

            migrationBuilder.RenameIndex(
                name: "IX_classArea_TeacherId",
                table: "classes",
                newName: "IX_classes_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_classes",
                table: "classes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_classes_teachers_TeacherId",
                table: "classes",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
