namespace TSheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeAuditTB")]
    public partial class EmployeeAuditTB
    {
        [Key]
        public int EmployeeAuditID { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string Comment { get; set; }

        public bool? IsApproved { get; set; }

        public int UserID { get; set; }

        public virtual Registration Registration { get; set; }
    }
}
