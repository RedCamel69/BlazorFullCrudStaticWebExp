using Api.Services.Admin.DataDumpService;
using Api.Services.Admin.UserService;
using Api.Services.BusinessService;
using Api.Services.CourseService;
using Api.Services.HelperService;
using Api.Services.LanguageService;
using Api.Services.StudentService;
using Api.Services.TutorService;
using BlazorEcommerceStaticWebApp.Api.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(BlazorEcommerceStaticWebApp.Api.StartUp))]
namespace BlazorEcommerceStaticWebApp.Api
{
    public class StartUp : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(builder.GetContext().ApplicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            var appSettingValue = config["StudentService-GetStudents"];

            if (Convert.ToBoolean(Environment.GetEnvironmentVariable("USE_AZURESQL")))
            {
                var value = Environment.GetEnvironmentVariable("ConnectionStringAzureSQL");

                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(value);
                    });
            }

            else
            {
                if (Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") != "Development")
                {
                    if (!File.Exists("D:\\home\\turin2.db"))
                    {

                        File.Copy("D:\\home\\site\\wwwroot\\turin2.db", "D:\\home\\turin2.db");
                        File.SetAttributes("D:\\home\\turin2.db", FileAttributes.Normal);
                    }

                    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        //options.UseSqlite(Utils.GetSQLiteConnectionString());
                        options.UseSqlite("Data source = D:\\home\\turin2.db");
                        //options.UseSqlite("Data source = D:\\home\\site\\wwwroot\\school.db");
                    });
                }
                else
                {
                    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlite(Utils.GetSQLiteConnectionString());
                    });

                }
            }

                //    var s = Utils.GetSQLiteConnectionString();

                //    bool isDevEnv = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == DevEnvValue ? true : false;
                //    // One time copy of the DB (per deployment)
                //    if (!isDevEnv && !File.Exists(Azure_DBPath))
                //        CopyDb();


                //    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                //    {
                //        Console.WriteLine("Dev dbContext");
                //        options.UseSqlite($"data source={(isDevEnv ? DBPath : Azure_DBPath)};");
                //    });

                //    //builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(
                //    //         (s, o) => o
                //    //           .UseSqlite($"data source={(isDevEnv ? DBPath : Azure_DBPath)};")
                //    //           //.UseLoggerFactory(s.GetRequiredService<ILoggerFactory>())
                //    //           );

                builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ITutorService, TutorService>();
            builder.Services.AddScoped<IBusinessService, BusinessService>();
            builder.Services.AddScoped<ILanguageService, LanguageService>();

            builder.Services.AddScoped<IHelperService, HelperService>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IDataDumpService, DataDumpService>();
           

        }

        //public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        //{
        //    base.ConfigureAppConfiguration(builder);
        //}

    }
}