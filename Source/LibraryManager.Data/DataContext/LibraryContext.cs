using LibraryManager.Data.Entities;
using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Reflection;

namespace LibraryManager.Data.DataContext
{
    public class LibraryContext : DbContext
    {
        private const string ConnectionStringName = "LibraryContextConnectionString";

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

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

        public void Seed()
        {
            // Load the SQL script from the embedded resource
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "LibraryManager.Data.StoredProcedures.CreateStoredProcedures.sql";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var sql = reader.ReadToEnd();
                    this.Database.ExecuteSqlCommand(sql);
                }
            }
        }

        private static string GetConnectionString()
        {
            var a = ConfigurationManager.ConnectionStrings;
            var connectionString = "Data Source=DESKTOP-2FMTBJB;Initial Catalog=LibraryDB;Integrated Security=True;TrustServerCertificate=True"; //ConfigurationManager.ConnectionStrings["LibraryContextConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"Failed to retrieve the connection string from the environment variable {ConnectionStringName}. Ensure that the environment variable is set with the correct connection string.");
            }

            return connectionString;
        }
    }
}
