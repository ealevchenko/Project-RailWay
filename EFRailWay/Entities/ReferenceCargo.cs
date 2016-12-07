namespace EFRailWay.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.ReferenceCargo")]
    public partial class ReferenceCargo
    {
        [Key]
        public int IDCargo { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string NameFull { get; set; }

        public int ETSNG { get; set; }
    }
}
