namespace EFRailWay.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.ReferenceStation")]
    public partial class ReferenceStation
    {
        [Key]
        public int IDStation { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Station { get; set; }

        [Required]
        [StringLength(250)]
        public string InternalRailroad { get; set; }

        [Required]
        [StringLength(10)]
        public string IR_Abbr { get; set; }

        [Required]
        [StringLength(250)]
        public string NameNetwork { get; set; }

        [Required]
        [StringLength(10)]
        public string NN_Abbr { get; set; }

        public int CodeCS { get; set; }
    }
}
