namespace TSheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NotificationsTB")]
    public partial class NotificationsTB
    {
        [Key]
        public int NotificationsID { get; set; }

        public int TimeSheetMasterID { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public virtual TimeSheetMaster TimeSheetMaster { get; set; }
    }
}
