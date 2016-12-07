namespace EFRailWay.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.ReferenceCountry")]
    public partial class ReferenceCountry
    {
        [Key]
        public int IDCountry { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        public int Code { get; set; }
    }
}
