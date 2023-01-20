using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class updatephoneTutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Tutors");

            migrationBuilder.AddColumn<string>(
                name: "MobilePhone",
                table: "Tutors",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobilePhone",
                table: "Tutors");

            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "Tutors",
                type: "INTEGER",
                nullable: true);
        }
    }
}
