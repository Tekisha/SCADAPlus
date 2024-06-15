namespace SCADA_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePasswordLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 30, storeType: "nvarchar"));
        }
    }
}
