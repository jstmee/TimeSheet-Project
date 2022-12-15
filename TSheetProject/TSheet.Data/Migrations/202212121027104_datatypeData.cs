namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatypeData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeSheetMaster", "TotalHours", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeSheetDetail", "Hours", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSheetDetail", "Hours", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TimeSheetMaster", "TotalHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
