namespace LibraryManager.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryManager.Data.DataContext.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibraryManager.Data.DataContext.LibraryContext context)
        {
            context.Seed();
        }
    }
}
