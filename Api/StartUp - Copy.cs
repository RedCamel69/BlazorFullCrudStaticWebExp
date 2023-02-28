using BlazorEcommerceStaticWebApp.Api.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(BlazorEcommerceStaticWebApp.Api.StartUp))]
namespace BlazorEcommerceStaticWebApp.Api
{
    public class StartUpx : FunctionsStartup
    {
        const string DevEnvValue = "Development";
        const string DBPath = "school.db";
        public const string Azure_DBPath = "D:\\home\\school.db";

        private static void CopyDb()
        {
            File.Copy(DBPath, Azure_DBPath);
            File.SetAttributes(Azure_DBPath, FileAttributes.Normal);
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {



            //bool isDevEnv = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == DevEnvValue ? true : false;

            //if(!isDevEnv && !File.Exists(Azure_DBPath))
            //{
            //    CopyDb();
            //}

            // if (isDevEnv)
            // {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                Console.WriteLine("Dev dbContext");
                options.UseSqlite(Utils.GetSQLiteConnectionString());
            });
            //}
            //else
            //{
            //    builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(
            //        (s, o) => o
            //        .UseSqlite("Data Source = school.db")
            //        //.UseSqlite("Data Source = D:\\home\\school.db")
            //        .UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()));
            //    //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    //{
            //    //    Console.WriteLine("Azure dbContext");
            //    //    options.UseSqlite($"Data Source = {(Azure_DBPath)}");
            //    //});
            //}
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            base.ConfigureAppConfiguration(builder);
        }

    }
}