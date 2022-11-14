namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEmployeeCodeinTB : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Registrations", "EmployeeCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Registrations", "EmployeeCode", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
        }
    }
}
