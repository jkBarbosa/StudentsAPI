using EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EFMigrationUtility
{
    public class ConsoleStartup
    {
        public ConsoleStartup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            //.. for test
            Console.WriteLine(Configuration.GetConnectionString("StudentsAppDbContext"));
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StudentsAppDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("StudentsAppDbContext");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            });
        }

        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
