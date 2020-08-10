namespace MovieStore.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedmorecolumnstoMovietable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Rated", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Movies", "Genre", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Movies", "Plot", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Movies", "Poster", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Movies", "ImdbRating", c => c.Single(nullable: false));
            AddColumn("dbo.Movies", "ImdbID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "ImdbID");
            DropColumn("dbo.Movies", "ImdbRating");
            DropColumn("dbo.Movies", "Poster");
            DropColumn("dbo.Movies", "Plot");
            DropColumn("dbo.Movies", "Genre");
            DropColumn("dbo.Movies", "Rated");
        }
    }
}
