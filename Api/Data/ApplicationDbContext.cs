using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerceStaticWebApp.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Tutor> Tutors { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(Utils.GetSQLiteConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

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

