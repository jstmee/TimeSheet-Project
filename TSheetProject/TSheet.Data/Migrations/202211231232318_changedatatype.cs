namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeSheetMaster", "Comment", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSheetMaster", "Comment", c => c.Binary(nullable: false, maxLength: 50));
        }
    }
}
