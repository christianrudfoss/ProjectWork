using ApplicationCore.Entities.ProjectAggregate;
using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Entities.WorkAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbProjectWorkContext: DbContext
    {
        public DbProjectWorkContext(DbContextOptions<DbProjectWorkContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Work>().ToTable("Work");

            //    base.OnModelCreating(builder);
            //    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
