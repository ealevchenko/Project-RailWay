namespace EFRailCars.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GRUZ_FRONTS
    {
        [Key]
        public int id_gruz_front { get; set; }

        public int? id_stat { get; set; }

        [StringLength(250)]
        public string name { get; set; }
    }
}
