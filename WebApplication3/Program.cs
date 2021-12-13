using Destructurama;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NpgsqlTypes;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .Build();

            // You may try to create different loggers from different instances of LoggerConfiguration.
            // Something like appsettingsForFile.json and appSettingsForConsole.json and read them separately.
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration, "Serilog")
                .Destructure.UsingAttributes() // ref https://github.com/destructurama/attributed#enabling-the-module
                .CreateLogger();

            try
            {
                Log.Information("Starting the server...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Exception in the application");
            }
            finally
            {
                Log.Information("Exiting the server...");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
