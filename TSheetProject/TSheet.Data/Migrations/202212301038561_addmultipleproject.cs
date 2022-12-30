namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmultipleproject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignedRoles", "AssignRole", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignedRoles", "AssignRole");
        }
    }
}
