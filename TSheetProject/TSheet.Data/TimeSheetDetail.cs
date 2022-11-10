namespace TSheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeSheetDetail")]
    public partial class TimeSheetDetail
    {
        public int TimeSheetDetailID { get; set; }

        public int Hours { get; set; }

        public int TimeSheetMasterID { get; set; }

        public DateTime Date { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual TimeSheetMaster TimeSheetMaster { get; set; }

        public virtual TimeSheetMaster TimeSheetMaster1 { get; set; }
    }
}
