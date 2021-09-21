using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using Autofac.Core;
using Core.Logger;
using Domain.Exception;
using Domain.IoC;
using EntityFramework.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;

namespace Api.Rest
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddUserSecrets<Startup>(false)
                .AddEnvironmentVariables();

            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));



            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        protected IContainer Container;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddControllers();
            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var modules = new List<IModule>
            {
                new RepositoryRegistry(),
                new SharedIoCRegistry(),
                
            };

            modules.ForEach(x => builder.RegisterModule(x));

            builder.Register(c => Container).AsSelf();
            builder.RegisterBuildCallback(c =>
            {
                Container = (IContainer)c;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
