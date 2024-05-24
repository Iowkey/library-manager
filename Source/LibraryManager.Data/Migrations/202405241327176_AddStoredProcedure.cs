namespace LibraryManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Reflection;

    public partial class AddStoredProcedure : DbMigration
    {
        public override void Up()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "LibraryManager.Data.StoredProcedures.CreateStoredProcedures.sql";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var sql = reader.ReadToEnd();
                Sql(sql);
            }
        }
        
        public override void Down()
        {
            Sql("DROP PROCEDURE IF EXISTS GetBooks");
        }
    }
}
