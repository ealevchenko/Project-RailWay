namespace EFRailWay.Entities.Reference
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.Code_Cargo")]
    public partial class Code_Cargo
    {
        public int ID { get; set; }

        public int IDETSNG { get; set; }

        [Required]
        [StringLength(250)]
        public string ETSNG { get; set; }

        public int IDGNG { get; set; }

        [Required]
        [StringLength(250)]
        public string GNG { get; set; }

        public int? IDSAP { get; set; }
    }
}
