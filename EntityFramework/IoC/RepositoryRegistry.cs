using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Core.CQRS;
using Core.DbConnection;
using EntityFramework.Factory;
using EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFramework.IoC
{
    public class RepositoryRegistry : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(StudentRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var opt = new DbContextOptionsBuilder<StudentsAppDbContext>();
                var connectionString = config.GetSection("ConnectionStrings:StudentsAppDbContext").Value;
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                return new StudentsAppDbContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<EfConnectionFactory>().As<IDbConnectionFactory>().InstancePerLifetimeScope();
        }
    }
}
