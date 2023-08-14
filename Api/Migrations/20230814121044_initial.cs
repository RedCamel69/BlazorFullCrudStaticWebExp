using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    BusinessId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.BusinessId);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "Tutors",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ProtopageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    MobilePhone = table.Column<string>(type: "TEXT", nullable: true),
                    BusinessId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.TutorId);
                    table.ForeignKey(
                        name: "FK_Tutors_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    NickName = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: true),
                    School = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StudentCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: true),
                    TutorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId");
                    table.ForeignKey(
                        name: "FK_Courses_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "TutorId");
                });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "BusinessId", "Name" },
                values: new object[] { 1, "Test Business 1" });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "BusinessId", "Name" },
                values: new object[] { 2, "Test Business 2" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "Code", "Name" },
                values: new object[] { 1, "en", "English" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "Code", "Name" },
                values: new object[] { 2, "fr", "French" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "Code", "Name" },
                values: new object[] { 3, "sp", "Spanish" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "FirstName", "LanguageId", "LastName", "NickName", "School" },
                values: new object[] { 1, "Bill", 1, "Smith", "Forest", "Green Fields Comp" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "FirstName", "LanguageId", "LastName", "NickName", "School" },
                values: new object[] { 2, "Arnold", 1, "Jones", "Arnie", "Green Fields Comp" });

            migrationBuilder.InsertData(
                table: "Tutors",
                columns: new[] { "TutorId", "BusinessId", "Email", "FirstName", "LastName", "MobilePhone", "Phone", "ProtopageUrl" },
                values: new object[] { 1, 1, "testemail@demo.com", "Bill", "Smith", "+44 0687 565665", "0161 454545", "https://www.protopage.co,/demo1" });

            migrationBuilder.InsertData(
                table: "Tutors",
                columns: new[] { "TutorId", "BusinessId", "Email", "FirstName", "LastName", "MobilePhone", "Phone", "ProtopageUrl" },
                values: new object[] { 2, 1, "fbrown@demo.com", "Frederick", "Brown", "+44 0688 565668", "0161 765432", "https://www.protopage.co,/demo2" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "EndDate", "LanguageId", "Name", "StartDate", "StudentCapacity", "TutorId" },
                values: new object[] { 1, new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "An Introduction to the Movies of Stanley Kubrick", new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 2 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "EndDate", "LanguageId", "Name", "StartDate", "StudentCapacity", "TutorId" },
                values: new object[] { 2, new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "An Introduction to the Movies of David Cronenberg", new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 180, 2 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "EndDate", "LanguageId", "Name", "StartDate", "StudentCapacity", "TutorId" },
                values: new object[] { 3, new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "An Introduction to the Movies of William Freidkin", new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1001, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_Name",
                table: "Businesses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LanguageId",
                table: "Courses",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TutorId",
                table: "Courses",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_LanguageId",
                table: "Students",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_BusinessId",
                table: "Tutors",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Tutors");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Businesses");
        }
    }
}
