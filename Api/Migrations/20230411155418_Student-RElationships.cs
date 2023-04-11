using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class StudentRElationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_LanguageId",
                table: "Students",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Languages_LanguageId",
                table: "Students",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Languages_LanguageId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_LanguageId",
                table: "Students");
        }
    }
}
