namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowdsomefieldnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Registrations", "DateOfbirth", c => c.DateTime());
            AlterColumn("dbo.Registrations", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Registrations", "IsActive", c => c.Boolean());
            AlterColumn("dbo.Registrations", "DateOfJoining", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Registrations", "DateOfJoining", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Registrations", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Registrations", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Registrations", "DateOfbirth", c => c.DateTime(nullable: false));
        }
    }
}
