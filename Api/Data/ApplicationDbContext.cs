using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;

namespace BlazorEcommerceStaticWebApp.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Tutor> Tutors { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Course> Courses { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //for migrations working with sqlite locally
       optionsBuilder.UseSqlite(Utils.GetSQLiteConnectionString());


        //if (!optionsBuilder.IsConfigured)
        //{
        //    //added to local.settings.json for dev , app configuration for cloud.
        //    //locally used the AAD connevction string, hosted the ADO.NET with admin password / userid
        //    var useAzureSQL = Convert.ToBoolean(Environment.GetEnvironmentVariable("USE_AZURESQL"));
        //    if (useAzureSQL)
        //    {
        //        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStringAzureSQL"));
        //    }
        //    else
        //    {
        //        optionsBuilder.UseSqlite(Utils.GetSQLiteConnectionString());
        //    }

        //}
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>()
            .HasData(
                new Language()
                {
                    LanguageId = 1,
                    Name = "English",
                    Code = "en"
                },
                new Language()
                {
                    LanguageId = 2,
                    Name = "French",
                    Code = "fr"
                },
                new Language()
                {
                    LanguageId = 3,
                    Name = "Spanish",
                    Code = "sp"
                }
                        );

        modelBuilder.Entity<Student>()
            .HasKey(x => new { x.StudentId });

        modelBuilder.Entity<Student>()
                    .HasData(
            new Student()
            {
                StudentId = 1,
                School = "Green Fields Comp",
                LanguageId = 1,
                FirstName = "Bill",
                LastName = "Smith",
                NickName = "Forest"
            },
                new Student()
                {
                    StudentId = 2,
                    School = "Green Fields Comp",
                    LanguageId = 1,
                    FirstName = "Arnold",
                    LastName = "Jones",
                    NickName = "Arnie"
                });

        modelBuilder.Entity<Business>()
            .HasKey(x => new { x.BusinessId });

        modelBuilder.Entity<Business>()
            .HasData(
                new Business() { BusinessId = 1, Name = "Test Business 1" },
                new Business() { BusinessId = 2, Name = "Test Business 2" }
            );

        modelBuilder.Entity<Tutor>()
            .HasKey(x => new { x.TutorId });

        modelBuilder.Entity<Tutor>()
                    .HasData(
            new Tutor()
            {
                TutorId = 1,
                FirstName = "Bill",
                LastName = "Smith",
                Email = "testemail@demo.com",
                MobilePhone = "+44 0687 565665",
                Phone = "0161 454545",
                ProtopageUrl = "https://www.protopage.co,/demo1",
                BusinessId = 1
            },
             new Tutor()
             {
                 TutorId = 2,
                 FirstName = "Frederick",
                 LastName = "Brown",
                 Email = "fbrown@demo.com",
                 MobilePhone = "+44 0688 565668",
                 Phone = "0161 765432",
                 ProtopageUrl = "https://www.protopage.co,/demo2",
                 BusinessId = 1
             });


        modelBuilder.Entity<Course>()
            .HasData(
                new Course()
                {
                    CourseId=1,
                    Name = "An Introduction to the Movies of Stanley Kubrick",
                    StartDate= new DateTime(2023, 8, 12),
                    EndDate= new DateTime(2023,2,24),
                    StudentCapacity=25,
                    LanguageId=1,
                    TutorId=2
                },
                new Course()
                {
                    CourseId = 2,
                    Name = "An Introduction to the Movies of David Cronenberg",
                    StartDate = new DateTime(2023, 8, 12),
                    EndDate = new DateTime(2023, 2, 24),
                    StudentCapacity=180,
                    LanguageId = 1,
                    TutorId = 2
                },
                new Course()
                {
                    CourseId = 3,
                    Name = "An Introduction to the Movies of William Freidkin",
                    StartDate = new DateTime(2023, 8, 12),
                    EndDate = new DateTime(2023, 2, 24),
                    StudentCapacity=1001,
                    LanguageId = 1,
                    TutorId = 2
                }
                        );

    }
}

