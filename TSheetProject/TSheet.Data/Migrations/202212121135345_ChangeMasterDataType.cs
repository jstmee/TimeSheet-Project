namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMasterDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeSheetMaster", "TotalHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSheetMaster", "TotalHours", c => c.Int(nullable: false));
        }
    }
}
