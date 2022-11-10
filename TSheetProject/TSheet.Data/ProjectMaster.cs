namespace TSheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectMaster")]
    public partial class ProjectMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectMaster()
        {
            DescriptionAndProjectMappings = new HashSet<DescriptionAndProjectMapping>();
            TimeSheetMasters = new HashSet<TimeSheetMaster>();
        }

        [Key]
        public int ProjectID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DescriptionAndProjectMapping> DescriptionAndProjectMappings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSheetMaster> TimeSheetMasters { get; set; }
    }
}
