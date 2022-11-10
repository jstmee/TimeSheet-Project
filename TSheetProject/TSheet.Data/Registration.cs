namespace TSheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Registration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Registration()
        {
            AssignedRoles = new HashSet<AssignedRole>();
            DescriptionAndProjectMappings = new HashSet<DescriptionAndProjectMapping>();
            EmployeeAuditTBs = new HashSet<EmployeeAuditTB>();
            TimeSheetAuditTBs = new HashSet<TimeSheetAuditTB>();
            TimeSheetMasters = new HashSet<TimeSheetMaster>();
        }

        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(12)]
        public string MobileNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string EmployeeCode { get; set; }

        public DateTime DateOfbirth { get; set; }

        [Required]
        [StringLength(50)]
        public string Gender { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [StringLength(50)]
        public string EditedBy { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DateOfLeaving { get; set; }

        public DateTime DateOfJoining { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignedRole> AssignedRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DescriptionAndProjectMapping> DescriptionAndProjectMappings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeAuditTB> EmployeeAuditTBs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSheetAuditTB> TimeSheetAuditTBs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSheetMaster> TimeSheetMasters { get; set; }
    }
}
