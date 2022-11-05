using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Entity;

namespace EFCodeFirstConsoleApp2
{
    public class SchoolDBContext : DbContext
    {
        public SchoolDBContext() : base("EmployeeDB2") //name=EmployeeDBConnectionString")
        {
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new CreateDatabaseIfNotExists<SchoolDBContext>()); 
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchoolDBContext, EFCodeFirstConsoleApp2.Migrations.Configuration>());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary key
            //modelBuilder.Entity<Student>().HasKey<int>(s => s.StudentId);

            // Configure Student & StudentAddress entity
            modelBuilder.Entity<Student>()
                           // Make Address as required in Student Table
                         //.HasRequired(s => s.Address)
                        .HasOptional(s => s.Address) // Mark Address property optional in Student entity
                        .WithRequired(ad => ad.Student);
            // mark Student property as required in StudentAddress entity.
            // Cannot save StudentAddress without Student
            // Map individual entity to a SP
            //modelBuilder.Entity<Student>().MapToStoredProcedures();

            // Maps all entties to use SPs for Insert, Update and Delete
            modelBuilder.Types().Configure(t => t.MapToStoredProcedures());
        }

    }
}
