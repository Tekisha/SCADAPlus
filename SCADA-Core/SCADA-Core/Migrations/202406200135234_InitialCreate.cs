namespace SCADA_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alarms",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        AlarmName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        TagId = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Limit = c.Double(nullable: false),
                        Type = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Acknowledged = c.Boolean(nullable: false),
                        Time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Description = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        IOAddress = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        InitialValue = c.Double(),
                        LowLimit = c.Double(),
                        HighLimit = c.Double(),
                        Units = c.String(unicode: false),
                        InitialValue1 = c.Double(),
                        Driver = c.String(unicode: false),
                        ScanTime = c.Int(),
                        OnOffScan = c.Boolean(),
                        LowLimit1 = c.Double(),
                        HighLimit1 = c.Double(),
                        Units1 = c.String(unicode: false),
                        Alarms = c.Boolean(),
                        TagType = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagValues",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        TagId = c.String(maxLength: 256, storeType: "nvarchar"),
                        Value = c.Double(nullable: false),
                        Time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tags", t => t.TagId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        Password = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagValues", "TagId", "dbo.Tags");
            DropIndex("dbo.TagValues", new[] { "TagId" });
            DropTable("dbo.Users");
            DropTable("dbo.TagValues");
            DropTable("dbo.Tags");
            DropTable("dbo.Alarms");
        }
    }
}
