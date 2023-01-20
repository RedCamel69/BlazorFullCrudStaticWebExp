using System;

namespace BlazorEcommerceStaticWebApp.Api.Data;

public class Utils
{
    public static string GetSQLiteConnectionString()
    {
        var home = Environment.GetEnvironmentVariable("HOME") ?? "";
        Console.WriteLine($"home: {home}");
        if (!string.IsNullOrEmpty(home))
        {
            home = System.IO.Path.Combine(home, "site", "wwwroot");
        }
        var databasePath = System.IO.Path.Combine(home, "turin.db");
        var connStr = $"Data Source={databasePath}";

        return connStr;
    }
}

