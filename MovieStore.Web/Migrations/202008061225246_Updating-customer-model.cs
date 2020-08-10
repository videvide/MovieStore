namespace MovieStore.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatingcustomermodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "BillingAddress", c => c.String(maxLength: 255));
            AlterColumn("dbo.Customers", "BillingCity", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "DeliveryAddress", c => c.String(maxLength: 255));
            AlterColumn("dbo.Customers", "DeliveryCity", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "DeliveryZip", c => c.String());
            AlterColumn("dbo.Customers", "PhoneNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "PhoneNo", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "DeliveryZip", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "DeliveryCity", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Customers", "DeliveryAddress", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "BillingCity", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Customers", "BillingAddress", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
