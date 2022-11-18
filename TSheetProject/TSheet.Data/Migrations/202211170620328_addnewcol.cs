namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectMaster", "ProjectDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectMaster", "ProjectDescription");
        }
    }
}
