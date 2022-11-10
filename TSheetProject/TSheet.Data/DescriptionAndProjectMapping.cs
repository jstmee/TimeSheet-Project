namespace TSheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DescriptionAndProjectMapping")]
    public partial class DescriptionAndProjectMapping
    {
        [Key]
        public int DescriptionID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int ProjectID { get; set; }

        public int UserID { get; set; }

        public virtual ProjectMaster ProjectMaster { get; set; }

        public virtual Registration Registration { get; set; }
    }
}
