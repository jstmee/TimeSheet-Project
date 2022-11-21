namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowdsomefieldnullableagain1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectMaster", "ProjectDescription", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectMaster", "ProjectDescription", c => c.String());
        }
    }
}
