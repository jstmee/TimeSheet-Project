namespace TSheet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AssignedRole
    {
        [Key]
        public int AssignedRolesID { get; set; }

        public int RoleID { get; set; }

        public int UserID { get; set; }

        public int CreatedById { get; set; }

        public int UpdatedById { get; set; }

        public virtual Registration Registration { get; set; }

        public virtual Role Role { get; set; }
        public int? AssignRole { get; set; }
    }
}
