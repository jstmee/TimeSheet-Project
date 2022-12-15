namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChnageDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeSheetDetail", "Hours", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSheetDetail", "Hours", c => c.Int());
        }
    }
}
