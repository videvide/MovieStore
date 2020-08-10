namespace MovieStore.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedbillingzipdatatypetostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "BillingZip", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "BillingZip", c => c.Int(nullable: false));
        }
    }
}
