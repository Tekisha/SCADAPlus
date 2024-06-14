namespace SCADA_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Description = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        IOAddress = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Driver = c.String(unicode: false),
                        ScanTime = c.Int(),
                        OnOffScan = c.Boolean(),
                        LowLimit = c.Double(),
                        HighLimit = c.Double(),
                        Units = c.String(unicode: false),
                        Alarms = c.Boolean(),
                        InitialValue = c.Double(),
                        LowLimit1 = c.Double(),
                        HighLimit1 = c.Double(),
                        Units1 = c.String(unicode: false),
                        Driver1 = c.String(unicode: false),
                        ScanTime1 = c.Int(),
                        OnOffScan1 = c.Boolean(),
                        InitialValue1 = c.Double(),
                        TagType = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Tags");
        }
    }
}
