namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowdsomefieldnullableagain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Registrations", "DateOfJoining", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TimeSheetDetail", "Hours", c => c.Int());
            AlterColumn("dbo.TimeSheetDetail", "Date", c => c.DateTime());
            AlterColumn("dbo.TimeSheetDetail", "CreatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSheetDetail", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TimeSheetDetail", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TimeSheetDetail", "Hours", c => c.Int(nullable: false));
            AlterColumn("dbo.Registrations", "DateOfJoining", c => c.DateTime());
        }
    }
}
