namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LittleChangeInRegistraionTb : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Registrations", name: "EditedBy", newName: "EditedById");
            RenameColumn(table: "dbo.Registrations", name: "CreatedBy", newName: "CreatedById");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Registrations", name: "CreatedById", newName: "CreatedBy");
            RenameColumn(table: "dbo.Registrations", name: "EditedById", newName: "EditedBy");
        }
    }
}
