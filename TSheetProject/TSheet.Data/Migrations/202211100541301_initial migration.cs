namespace TSheet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedRoles",
                c => new
                    {
                        AssignedRolesID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        UpdatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssignedRolesID)
                .ForeignKey("dbo.Registrations", t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 200, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                        MobileNumber = c.String(nullable: false, maxLength: 12, unicode: false),
                        EmployeeCode = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        DateOfbirth = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 50, unicode: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        EditedBy = c.String(maxLength: 50, unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        DateOfLeaving = c.DateTime(),
                        DateOfJoining = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.DescriptionAndProjectMapping",
                c => new
                    {
                        DescriptionID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 200, unicode: false),
                        ProjectID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DescriptionID)
                .ForeignKey("dbo.ProjectMaster", t => t.ProjectID)
                .ForeignKey("dbo.Registrations", t => t.UserID)
                .Index(t => t.ProjectID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ProjectMaster",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.TimeSheetMaster",
                c => new
                    {
                        TimeSheetMasterID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        TotalHours = c.Int(nullable: false),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        Comment = c.Binary(nullable: false, maxLength: 50),
                        TimeSheetStatus = c.String(maxLength: 50, unicode: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TimeSheetMasterID)
                .ForeignKey("dbo.ProjectMaster", t => t.ProjectId)
                .ForeignKey("dbo.Registrations", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.NotificationsTB",
                c => new
                    {
                        NotificationsID = c.Int(nullable: false, identity: true),
                        TimeSheetMasterID = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.NotificationsID)
                .ForeignKey("dbo.TimeSheetMaster", t => t.TimeSheetMasterID)
                .Index(t => t.TimeSheetMasterID);
            
            CreateTable(
                "dbo.TimeSheetDetail",
                c => new
                    {
                        TimeSheetDetailID = c.Int(nullable: false, identity: true),
                        Hours = c.Int(nullable: false),
                        TimeSheetMasterID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TimeSheetDetailID)
                .ForeignKey("dbo.TimeSheetMaster", t => t.TimeSheetMasterID)
                .Index(t => t.TimeSheetMasterID);
            
            CreateTable(
                "dbo.EmployeeAuditTB",
                c => new
                    {
                        EmployeeAuditID = c.Int(nullable: false, identity: true),
                        Status = c.String(maxLength: 50, unicode: false),
                        Comment = c.String(maxLength: 50, unicode: false),
                        IsApproved = c.Boolean(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeAuditID)
                .ForeignKey("dbo.Registrations", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.TimeSheetAuditTB",
                c => new
                    {
                        TimeSheetAuditID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 50, unicode: false),
                        ApprovedBy = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.TimeSheetAuditID)
                .ForeignKey("dbo.Registrations", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedRoles", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.TimeSheetMaster", "UserID", "dbo.Registrations");
            DropForeignKey("dbo.TimeSheetAuditTB", "UserID", "dbo.Registrations");
            DropForeignKey("dbo.EmployeeAuditTB", "UserID", "dbo.Registrations");
            DropForeignKey("dbo.DescriptionAndProjectMapping", "UserID", "dbo.Registrations");
            DropForeignKey("dbo.TimeSheetMaster", "ProjectId", "dbo.ProjectMaster");
            DropForeignKey("dbo.TimeSheetDetail", "TimeSheetMasterID", "dbo.TimeSheetMaster");
            DropForeignKey("dbo.NotificationsTB", "TimeSheetMasterID", "dbo.TimeSheetMaster");
            DropForeignKey("dbo.DescriptionAndProjectMapping", "ProjectID", "dbo.ProjectMaster");
            DropForeignKey("dbo.AssignedRoles", "UserID", "dbo.Registrations");
            DropIndex("dbo.TimeSheetAuditTB", new[] { "UserID" });
            DropIndex("dbo.EmployeeAuditTB", new[] { "UserID" });
            DropIndex("dbo.TimeSheetDetail", new[] { "TimeSheetMasterID" });
            DropIndex("dbo.NotificationsTB", new[] { "TimeSheetMasterID" });
            DropIndex("dbo.TimeSheetMaster", new[] { "ProjectId" });
            DropIndex("dbo.TimeSheetMaster", new[] { "UserID" });
            DropIndex("dbo.DescriptionAndProjectMapping", new[] { "UserID" });
            DropIndex("dbo.DescriptionAndProjectMapping", new[] { "ProjectID" });
            DropIndex("dbo.AssignedRoles", new[] { "UserID" });
            DropIndex("dbo.AssignedRoles", new[] { "RoleID" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Roles");
            DropTable("dbo.TimeSheetAuditTB");
            DropTable("dbo.EmployeeAuditTB");
            DropTable("dbo.TimeSheetDetail");
            DropTable("dbo.NotificationsTB");
            DropTable("dbo.TimeSheetMaster");
            DropTable("dbo.ProjectMaster");
            DropTable("dbo.DescriptionAndProjectMapping");
            DropTable("dbo.Registrations");
            DropTable("dbo.AssignedRoles");
        }
    }
}
