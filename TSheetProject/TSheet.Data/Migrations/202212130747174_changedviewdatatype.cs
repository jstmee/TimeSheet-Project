namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedviewdatatype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSheetAuditTB", "TimeSheetDetailID", c => c.Int());
            AlterColumn("dbo.TimeSheetMaster", "TotalHours", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.TimeSheetAuditTB", "TimeSheetDetailID");
            AddForeignKey("dbo.TimeSheetAuditTB", "TimeSheetDetailID", "dbo.TimeSheetDetail", "TimeSheetDetailID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeSheetAuditTB", "TimeSheetDetailID", "dbo.TimeSheetDetail");
            DropIndex("dbo.TimeSheetAuditTB", new[] { "TimeSheetDetailID" });
            AlterColumn("dbo.TimeSheetMaster", "TotalHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.TimeSheetAuditTB", "TimeSheetDetailID");
        }
    }
}
