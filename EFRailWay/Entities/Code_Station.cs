namespace EFRailWay.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.Code_Station")]
    public partial class Code_Station
    {
        [Key]
        public int IDStation { get; set; }

        public int Code { get; set; }

        public int? CodeCS { get; set; }

        [Required]
        [StringLength(50)]
        public string Station { get; set; }

        public int? IDInternalRailroad { get; set; }
    }
}
