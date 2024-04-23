namespace ProjektASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressModels", "Code", c => c.String(maxLength: 6));
            AddColumn("dbo.AddressModels", "Address", c => c.String());
            AddColumn("dbo.AddressModels", "HouseNumber", c => c.Int(nullable: false));
            DropColumn("dbo.AddressModels", "ZipCode");
            DropColumn("dbo.AddressModels", "StreetAndBuildingNumber");
            DropColumn("dbo.AddressModels", "ApartmentNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AddressModels", "ApartmentNumber", c => c.Int(nullable: false));
            AddColumn("dbo.AddressModels", "StreetAndBuildingNumber", c => c.String());
            AddColumn("dbo.AddressModels", "ZipCode", c => c.String(maxLength: 6));
            DropColumn("dbo.AddressModels", "HouseNumber");
            DropColumn("dbo.AddressModels", "Address");
            DropColumn("dbo.AddressModels", "Code");
        }
    }
}
