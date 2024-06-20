namespace SCADA_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tags", "Driver");
            DropColumn("dbo.Tags", "ScanTime");
            DropColumn("dbo.Tags", "OnOffScan");
            RenameColumn(table: "dbo.Tags", name: "LowLimit", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Tags", name: "HighLimit", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.Tags", name: "Units", newName: "__mig_tmp__2");
            RenameColumn(table: "dbo.Tags", name: "LowLimit1", newName: "LowLimit");
            RenameColumn(table: "dbo.Tags", name: "HighLimit1", newName: "HighLimit");
            RenameColumn(table: "dbo.Tags", name: "Units1", newName: "Units");
            RenameColumn(table: "dbo.Tags", name: "Driver1", newName: "Driver");
            RenameColumn(table: "dbo.Tags", name: "ScanTime1", newName: "ScanTime");
            RenameColumn(table: "dbo.Tags", name: "OnOffScan1", newName: "OnOffScan");
            RenameColumn(table: "dbo.Tags", name: "__mig_tmp__0", newName: "LowLimit1");
            RenameColumn(table: "dbo.Tags", name: "__mig_tmp__1", newName: "HighLimit1");
            RenameColumn(table: "dbo.Tags", name: "__mig_tmp__2", newName: "Units1");
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagValues", "TagId", "dbo.Tags");
            DropIndex("dbo.TagValues", new[] { "TagId" });
            DropTable("dbo.TagValues");
            DropTable("dbo.Alarms");
            RenameColumn(table: "dbo.Tags", name: "Units1", newName: "__mig_tmp__2");
            RenameColumn(table: "dbo.Tags", name: "HighLimit1", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.Tags", name: "LowLimit1", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Tags", name: "OnOffScan", newName: "OnOffScan1");
            RenameColumn(table: "dbo.Tags", name: "ScanTime", newName: "ScanTime1");
            RenameColumn(table: "dbo.Tags", name: "Driver", newName: "Driver1");
            RenameColumn(table: "dbo.Tags", name: "Units", newName: "Units1");
            RenameColumn(table: "dbo.Tags", name: "HighLimit", newName: "HighLimit1");
            RenameColumn(table: "dbo.Tags", name: "LowLimit", newName: "LowLimit1");
            RenameColumn(table: "dbo.Tags", name: "__mig_tmp__2", newName: "Units");
            RenameColumn(table: "dbo.Tags", name: "__mig_tmp__1", newName: "HighLimit");
            RenameColumn(table: "dbo.Tags", name: "__mig_tmp__0", newName: "LowLimit");
            AddColumn("dbo.Tags", "OnOffScan", c => c.Boolean());
            AddColumn("dbo.Tags", "ScanTime", c => c.Int());
            AddColumn("dbo.Tags", "Driver", c => c.String(unicode: false));
        }
    }
}
