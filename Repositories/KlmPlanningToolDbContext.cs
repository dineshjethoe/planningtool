using Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Repositories
{
    /// <summary>
    /// This context class intermediates between the enities (models/domain objects) and the database.
    /// </summary>
    public class KlmPlanningToolDbContext : DbContext
    {
        public KlmPlanningToolDbContext() : base("KlmPlanningToolDbContext")
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Using the fluent API of EF to set the schema names of the tables in the database.
            modelBuilder.Entity<Task>().ToTable("Tasks", "Task");
            modelBuilder.Entity<Employee>().ToTable("Employees", "Employee");
            modelBuilder.Entity<AssignedTask>().ToTable("AssignedTasks", "Employee");

            //Conversion of a datetime2 data type to a datetime data type resulted in an out-of-range value.
            //Using custom conventions to specify the mapping globally (EF6+)
            //Also change the datatime datatype in the database to datetime2
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            //The DateTimeCreated columns in the database tables have default value (constraint).
            //The default value is SQL Server built-in getdate() function.
            modelBuilder
                .Entity<Task>()
                .Property(t => t.DateTimeCreated)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder
                .Entity<Employee>()
                .Property(t => t.DateTimeCreated)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder
                .Entity<AssignedTask>()
                .Property(t => t.DateTimeCreated)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            base.OnModelCreating(modelBuilder);
        }
    }
}
