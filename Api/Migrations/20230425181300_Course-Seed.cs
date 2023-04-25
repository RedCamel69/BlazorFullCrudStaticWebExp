using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class CourseSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "EndDate", "LanguageId", "Name", "StartDate", "TutorId" },
                values: new object[] { 1, new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "An Introduction to the Movies of Stanley Kubrick", new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "EndDate", "LanguageId", "Name", "StartDate", "TutorId" },
                values: new object[] { 2, new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "An Introduction to the Movies of David Cronenberg", new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "EndDate", "LanguageId", "Name", "StartDate", "TutorId" },
                values: new object[] { 3, new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "An Introduction to the Movies of William Freidkin", new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3);
        }
    }
}
