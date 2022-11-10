namespace TSheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeSheetAuditTB")]
    public partial class TimeSheetAuditTB
    {
        [Key]
        public int TimeSheetAuditID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string ApprovedBy { get; set; }

        public virtual Registration Registration { get; set; }
    }
}
