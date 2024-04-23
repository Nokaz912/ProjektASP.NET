namespace ProjektASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Payment", c => c.String());
            AddColumn("dbo.Orders", "Shipping", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Shipping");
            DropColumn("dbo.Orders", "Payment");
        }
    }
}
