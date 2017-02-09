namespace EFRailWay.Entities.Reference
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.Code_Country")]
    public partial class Code_Country
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        [StringLength(2)]
        public string Alpha_2 { get; set; }

        [Required]
        [StringLength(3)]
        public string Alpha_3 { get; set; }

        public int Code { get; set; }



        [Required]
        [StringLength(20)]
        public string ISO3166_2 { get; set; }

        public int? IDState { get; set; }

        public int? CodeEurope { get; set; }

    }
}
