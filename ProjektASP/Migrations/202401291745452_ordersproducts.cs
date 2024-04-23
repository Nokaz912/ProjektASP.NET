namespace ProjektASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordersproducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrdersProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrdersProducts");
        }
    }
}
