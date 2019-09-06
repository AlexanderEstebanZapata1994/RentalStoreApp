namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatingGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name, Description) VALUES ('Comedia', NULL);");
            Sql("INSERT INTO Genres (Name, Description) VALUES ('Terror', NULL);");
            Sql("INSERT INTO Genres (Name, Description) VALUES ('Suspenso', NULL);");
        }
        
        public override void Down()
        {
        }
    }
}
