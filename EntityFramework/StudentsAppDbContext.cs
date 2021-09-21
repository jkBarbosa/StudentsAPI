using Domain.Models;
using EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Core.CQRS;
using Core.DbConnection;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFramework
{
    public class StudentsAppDbContext : DbContext
    {
        public StudentsAppDbContext() : base()
        {
        }

        public StudentsAppDbContext(DbContextOptions<StudentsAppDbContext> options) : base(options)
        {
        }

        #region DbSets

        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //If we haven't already been configured, use the local config
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    //.AddEnvironmentVariables()
                    //.AddUserSecrets<Program>()
                    .Build();

                var connectionString = Configuration.GetConnectionString("StudentsAppDbContext");

                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                //optionsBuilder.EnableSensitiveDataLogging();
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
                x => x.Namespace == typeof(StudentConfig).Namespace);

            //This will only pull those Entities who derive from BaseEntity, which contain the ModifiedOn, CreatedOn, leaving higher level
            //derivatives (such as Audit, which is based on BaseEntity Parent IdEntity).
            foreach (var each in modelBuilder.Model.GetEntityTypes()
                .Where(x => x.ClrType.IsSubclassOf(typeof(BaseEntity))))
            {
                modelBuilder.Entity(each.Name).Property<DateTime>("ModifiedOn")
                    .ValueGeneratedOnAddOrUpdate();

                modelBuilder.Entity(each.Name).Property<DateTime>("CreatedOn")
                    .ValueGeneratedOnAdd();
            }
        }
    }
}
