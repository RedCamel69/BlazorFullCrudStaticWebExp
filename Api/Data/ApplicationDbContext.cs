using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerceStaticWebApp.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Tutor> Tutors { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Language> Languages { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(Utils.GetSQLiteConnectionString());
        }
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
                StudentId=1,
                 School= "Green Fields Comp",
                LanguageId=1,
                FirstName = "Bill",
                LastName = "Smith",
                NickName="Forest"
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

        //modelBuilder.Entity<Business>()
        //    .HasData(
        //        new Business() { BusinessId = 1, Name = "Test Business 1" },
        //        new Business() { BusinessId = 2, Name = "Test Business 2" }
        //    );

        modelBuilder.Entity<Tutor>()
            .HasKey(x => new { x.Id });

        //modelBuilder.Entity<Tutor>()
        //            .HasData(
        //    new Tutor() { 
        //        Id = 1, 
        //        FirstName = "Bill",
        //        LastName = "Smith", 
        //        Email = "testemail@demo.com", 
        //        MobilePhone = "+44 0687 565665", 
        //        Phone = "0161 454545", 
        //        ProtopageUrl = "https://www.protopage.co,/demo1", 
        //        BusinessId = 1});




    }
}

