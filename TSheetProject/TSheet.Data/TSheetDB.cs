using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TSheet.Data
{
    public partial class TSheetDB : DbContext
    {
        public TSheetDB()
            : base("name=TSheetDB")
        {
        }

        public virtual DbSet<AssignedRole> AssignedRoles { get; set; }
        public virtual DbSet<DescriptionAndProjectMapping> DescriptionAndProjectMappings { get; set; }
        public virtual DbSet<EmployeeAuditTB> EmployeeAuditTBs { get; set; }
        public virtual DbSet<NotificationsTB> NotificationsTBs { get; set; }
        public virtual DbSet<ProjectMaster> ProjectMasters { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TimeSheetAuditTB> TimeSheetAuditTBs { get; set; }
        public virtual DbSet<TimeSheetDetail> TimeSheetDetails { get; set; }
        public virtual DbSet<TimeSheetMaster> TimeSheetMasters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DescriptionAndProjectMapping>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeAuditTB>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeAuditTB>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<NotificationsTB>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectMaster>()
                .Property(e => e.ProjectName)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectMaster>()
                .HasMany(e => e.DescriptionAndProjectMappings)
                .WithRequired(e => e.ProjectMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectMaster>()
                .HasMany(e => e.TimeSheetMasters)
                .WithRequired(e => e.ProjectMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.EmployeeCode)
                .IsFixedLength();

            modelBuilder.Entity<Registration>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.EditedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Registration>()
                .HasMany(e => e.AssignedRoles)
                .WithRequired(e => e.Registration)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registration>()
                .HasMany(e => e.DescriptionAndProjectMappings)
                .WithRequired(e => e.Registration)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registration>()
                .HasMany(e => e.EmployeeAuditTBs)
                .WithRequired(e => e.Registration)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registration>()
                .HasMany(e => e.TimeSheetAuditTBs)
                .WithRequired(e => e.Registration)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registration>()
                .HasMany(e => e.TimeSheetMasters)
                .WithRequired(e => e.Registration)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.AssignedRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeSheetAuditTB>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<TimeSheetAuditTB>()
                .Property(e => e.ApprovedBy)
                .IsUnicode(false);

            modelBuilder.Entity<TimeSheetMaster>()
                .Property(e => e.TimeSheetStatus)
                .IsUnicode(false);

            modelBuilder.Entity<TimeSheetMaster>()
                .HasMany(e => e.NotificationsTBs)
                .WithRequired(e => e.TimeSheetMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeSheetMaster>()
                .HasMany(e => e.TimeSheetDetails)
                .WithRequired(e => e.TimeSheetMaster)
                .HasForeignKey(e => e.TimeSheetMasterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeSheetMaster>()
                .HasMany(e => e.TimeSheetDetails1)
                .WithRequired(e => e.TimeSheetMaster1)
                .HasForeignKey(e => e.TimeSheetMasterID)
                .WillCascadeOnDelete(false);
        }
    }
}
