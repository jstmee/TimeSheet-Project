namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablecolumnadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSheetAuditTB", "TimeSheetDetailID", c => c.Int(nullable: false));
            CreateIndex("dbo.TimeSheetAuditTB", "TimeSheetDetailID");
            AddForeignKey("dbo.TimeSheetAuditTB", "TimeSheetDetailID", "dbo.TimeSheetDetail", "TimeSheetDetailID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeSheetAuditTB", "TimeSheetDetailID", "dbo.TimeSheetDetail");
            DropIndex("dbo.TimeSheetAuditTB", new[] { "TimeSheetDetailID" });
            DropColumn("dbo.TimeSheetAuditTB", "TimeSheetDetailID");
        }
    }
}
