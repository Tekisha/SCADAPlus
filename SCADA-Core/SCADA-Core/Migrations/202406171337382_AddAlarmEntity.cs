namespace SCADA_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAlarmEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alarms",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        AlarmName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        TagId = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Limit = c.Double(nullable: false),
                        Type = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Acknowledged = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Alarms");
        }
    }
}
