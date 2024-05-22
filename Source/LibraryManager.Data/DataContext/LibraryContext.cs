using LibraryManager.Data.Entities;
using System;
using System.Configuration;
using System.Data.Entity;

namespace LibraryManager.Data.DataContext
{
    public class LibraryContext : DbContext
    {
        private const string ConnectionStringName = "LibraryContextConnectionString";

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public LibraryContext() : base(GetConnectionString())
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LibraryContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasRequired(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            base.OnModelCreating(modelBuilder);
        }

        private static string GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"Failed to retrieve the connection string from the environment variable {ConnectionStringName}. Ensure that the environment variable is set with the correct connection string.");
            }

            return connectionString;
        }
    }
}
