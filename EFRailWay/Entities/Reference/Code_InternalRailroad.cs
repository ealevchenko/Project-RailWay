namespace EFRailWay.Entities.Reference
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.Code_InternalRailroad")]
    public partial class Code_InternalRailroad
    {
        [Key]
        public int IDInternalRailroad { get; set; }

        public int IDState { get; set; }

        [Required]
        [StringLength(250)]
        public string InternalRailroad { get; set; }

        public int Code { get; set; }

        [Required]
        [StringLength(10)]
        public string Abbr { get; set; }

        [Required]
        [StringLength(300)]
        public string StationsCodes { get; set; }

        public virtual Code_State Code_State { get; set; }
    }
}
