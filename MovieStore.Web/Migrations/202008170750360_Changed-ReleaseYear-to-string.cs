namespace MovieStore.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedReleaseYeartostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "ReleaseYear", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "ReleaseYear", c => c.Int(nullable: false));
        }
    }
}
