using System;
using System.IO;
using EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace EFMigrationUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Applying migrations");
            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ConsoleStartup>()
                .Build();

            using (var context = (StudentsAppDbContext)
                webHost.Services.GetService(typeof(StudentsAppDbContext)))
            {
                context.Database.Migrate();
            }

            Console.WriteLine("Done");
        }
    }
}
