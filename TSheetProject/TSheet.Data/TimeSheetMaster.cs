namespace TSheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeSheetMaster")]
    public partial class TimeSheetMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeSheetMaster()
        {
            NotificationsTBs = new HashSet<NotificationsTB>();
            TimeSheetDetails = new HashSet<TimeSheetDetail>();
            TimeSheetDetails1 = new HashSet<TimeSheetDetail>();
        }

        public int TimeSheetMasterID { get; set; }

        public int UserID { get; set; }

        public decimal? TotalHours { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Comment { get; set; }

        [StringLength(50)]
        public string TimeSheetStatus { get; set; }

        public int ProjectId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationsTB> NotificationsTBs { get; set; }

        public virtual ProjectMaster ProjectMaster { get; set; }

        public virtual Registration Registration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSheetDetail> TimeSheetDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSheetDetail> TimeSheetDetails1 { get; set; }
    }
}
