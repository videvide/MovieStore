namespace MovieStore.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlotMaxCharacters : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Plot", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Plot", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
