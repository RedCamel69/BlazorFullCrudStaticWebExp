﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using BlazorEcommerceStaticWebApp.Api.Data;
using System.IO;
using System;
using Microsoft.Extensions.Logging;
using Api.Services.ProductService;
using Api.Services.CategoryService;
using Api.Services.TutorService;

[assembly: FunctionsStartup(typeof(BlazorEcommerceStaticWebApp.Api.StartUp))]
namespace BlazorEcommerceStaticWebApp.Api
{
    public class StartUp : FunctionsStartup
    {

      

        public override void Configure(IFunctionsHostBuilder builder)
        {
            if (Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") != "Development")
            {
                if (!File.Exists("D:\\home\\turin.db"))
                {

                    File.Copy("D:\\home\\site\\wwwroot\\turin.db", "D:\\home\\turin.db");
                    File.SetAttributes("D:\\home\\turin.db", FileAttributes.Normal);
                }

                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    //options.UseSqlite(Utils.GetSQLiteConnectionString());
                    options.UseSqlite("Data source = D:\\home\\turin.db");
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

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<ITutorService, TutorService>();

        }

        //public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        //{
        //    base.ConfigureAppConfiguration(builder);
        //}

    }
}